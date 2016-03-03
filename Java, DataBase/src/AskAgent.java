import javax.swing.JFrame;
import javax.swing.GroupLayout;
import javax.swing.GroupLayout.Alignment;
import javax.swing.DefaultListModel;
import javax.swing.JScrollPane;
import javax.swing.JList;
import javax.swing.JButton;
import javax.swing.LayoutStyle.ComponentPlacement;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.io.FileInputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import javax.swing.event.ListSelectionListener;
import javax.swing.event.ListSelectionEvent;


@SuppressWarnings("serial")
public class AskAgent extends JFrame{
	
	public TovarMoving tovarMoving;
	private DefaultListModel<String> listModel;
	private List<Integer> idents;
	private JButton button;
	
	/**
	 * Create the application.
	 */
	public AskAgent(TovarMoving tovarMoving) {
		this.tovarMoving = tovarMoving;
		initialize();
	}
	
	public void refresh(){
		listModel.removeAllElements();
		idents.clear();
		
		Properties pr = new Properties();
	    try{
	    	FileInputStream inp = new FileInputStream("database.prop");
	    	pr.load(inp);
	    	inp.close();
	    } catch (IOException e) {return;}
	  
	    String databaseURL=pr.getProperty("dbURL");
	    
	    String user =pr.getProperty("user");
	    
	    String password =pr.getProperty("password");
	    String driverName = pr.getProperty("driver");
	    
	    Connection c = null;
	    Statement s = null;
	    ResultSet rs = null;

	    try{
	    	Class.forName(driverName);
	    	c = DriverManager.getConnection(databaseURL,user,password);
	    	c.getMetaData();
	    	s=c.createStatement();
	    	rs = s.executeQuery("select ID_AG, NAME_AG from AGENT");
	    	while (rs.next()) {
	    	 //String rightString= new String(badString.getBytes("windows-1251"),"utf-8");
	    		idents.add(rs.getInt("ID_AG"));
	    		String name_ag = new String(rs.getString("NAME_AG").toString().getBytes(),"utf-8").trim();
	    		listModel.addElement(name_ag);
	    	}
	    }
	    catch(ClassNotFoundException e){
	    	System.out.println("Fireberd JDBC driver not found");
	    }
	    catch(SQLException e){
	    	System.out.println("SQLException" +e.getMessage());
	    }
	    catch(Exception e) {
	    	System.out.println("Exception" +e.getMessage());
	    }
	    finally{
	    	try{  if (rs!=null) rs.close();} catch(SQLException e){}
	    	try{  if (s!=null)  s.close();} catch(SQLException e){}
	    	try{  if (c!=null)  c.close();} catch(SQLException e){}
	    }
		button.setEnabled(false);
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		listModel = new DefaultListModel<String>();
		idents = new ArrayList<Integer>();
		
		setTitle("\u0412\u044B\u0431\u043E\u0440 \u0430\u0433\u0435\u043D\u0442\u0430");
		setBounds(100, 100, 317, 324);
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
		
		JScrollPane scrollPane = new JScrollPane();
		
		button = new JButton("\u041E\u041A");
		JButton button_1 = new JButton("\u041E\u0442\u043C\u0435\u043D\u0430");
		JButton button_2 = new JButton("\u041E\u0431\u043D\u043E\u0432\u0438\u0442\u044C");
		
		GroupLayout groupLayout = new GroupLayout(getContentPane());
		groupLayout.setHorizontalGroup(
			groupLayout.createParallelGroup(Alignment.LEADING)
				.addGroup(Alignment.TRAILING, groupLayout.createSequentialGroup()
					.addContainerGap()
					.addGroup(groupLayout.createParallelGroup(Alignment.TRAILING)
						.addComponent(scrollPane, Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 281, Short.MAX_VALUE)
						.addGroup(groupLayout.createSequentialGroup()
							.addComponent(button_2)
							.addPreferredGap(ComponentPlacement.RELATED, 68, Short.MAX_VALUE)
							.addComponent(button_1)
							.addPreferredGap(ComponentPlacement.RELATED)
							.addComponent(button)))
					.addContainerGap())
		);
		groupLayout.setVerticalGroup(
			groupLayout.createParallelGroup(Alignment.LEADING)
				.addGroup(Alignment.TRAILING, groupLayout.createSequentialGroup()
					.addContainerGap()
					.addComponent(scrollPane, GroupLayout.DEFAULT_SIZE, 235, Short.MAX_VALUE)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addGroup(groupLayout.createParallelGroup(Alignment.BASELINE)
						.addComponent(button)
						.addComponent(button_1)
						.addComponent(button_2))
					.addContainerGap())
		);
		
		JList<String> list = new JList<String>(listModel);
		list.addListSelectionListener(new ListSelectionListener() {
			public void valueChanged(ListSelectionEvent arg0) {
				button.setEnabled(true);
			}
		});
		scrollPane.setViewportView(list);
		getContentPane().setLayout(groupLayout);
		
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(WindowEvent arg0) {
				/*if (Result != -1)
					setVisible(false);*/
			}
		});
		button.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				setVisible(false);
				tovarMoving.SetAgentAndShow(
						idents.get(list.getSelectedIndex()),
						listModel.get(list.getSelectedIndex()));
			}
		});
		button_1.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				setVisible(false);
			}
		});
		button_2.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				refresh();
			}
		});
		addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent e) {
				if (listModel.size() == 0)
					refresh();
			}
		});
	}

}
