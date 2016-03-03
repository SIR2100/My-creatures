import javax.swing.JFrame;
import javax.swing.GroupLayout;
import javax.swing.GroupLayout.Alignment;
import java.io.FileInputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Properties;

import javax.swing.DefaultListModel;
import javax.swing.JScrollPane;
import javax.swing.JList;
import javax.swing.JButton;
import javax.swing.LayoutStyle.ComponentPlacement;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

import javax.swing.JLabel;


@SuppressWarnings("serial")
public class TovarMoving extends JFrame{


	/**
	 * Create the application.
	 */
	public TovarMoving() {
		setTitle("\u0414\u0432\u0438\u0436\u0435\u043D\u0438\u0435 \u0442\u043E\u0432\u0430\u0440\u043E\u0432");
		initialize();
	}
	public void Show(){
		if (idAgent == -1)
		{
			askAgent.setVisible(!askAgent.isVisible());
			return;
		}
		setVisible(!isVisible());
		refresh();
		
	}
	public void SetAgentAndShow(int id, String name){
		idAgent = id;
		label.setText("Выбранный агент: \"" + name + "\"");
		
		if	(!isVisible())
		{
			setVisible(true);
			refresh();
		}
			
	}
	
	private AskAgent askAgent;
	private DefaultListModel<String> listModel;
	private int idAgent = -1;
	private JLabel label;
	
	private void refresh(){
		listModel.removeAllElements();
		
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
	    	rs = s.executeQuery("select * from FIND_DEFICIT");
	    	while (rs.next()) {
	    	 //String rightString= new String(badString.getBytes("windows-1251"),"utf-8");
	    		String concat = new String(rs.getString("res").getBytes(),"utf-8").trim();
	    		listModel.addElement(concat);
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
	}
	
	private void update() {
		listModel.removeAllElements();
				
	    Properties pr = new Properties();
	    try{
	    	FileInputStream inp = new FileInputStream("database.prop");
	    	pr.load(inp);
	    	inp.close();
	    } catch (IOException e) {return;}
	  
	    Properties props = new Properties();
	    props.setProperty("user", pr.getProperty("user"));
	    props.setProperty("password", pr.getProperty("password"));
	    props.setProperty("TRANSACTION_READ_COMMITTED",
	     "isc_tpb_read_committed," +
	     "isc_tpb_write,isc_tpb_wait");
	   // Connection connection = DriverManager.getConnection("jdbc:firebirdsql:localhost/3050:c:/example.fdb", props);

	    String databaseURL =pr.getProperty("dbURL");
	    String driverName = pr.getProperty("driver");
	    
	    Connection c = null;
	    Statement s = null;
	    ResultSet rs = null;
	
	    try{
	    	Class.forName(driverName);
	    	c = DriverManager.getConnection(databaseURL, props);
	    	c.getMetaData();
	    	c.setAutoCommit(false);
	    	s = c.createStatement();
	    	rs = s.executeQuery("select * from FILL_WAREHOUSE("+idAgent+")");
	    	while (rs.next()) {
		    	String bad_pair = new String(rs.getString("BAD_PAIR").getBytes(),"utf-8").trim();
		    	listModel.addElement("Невозможно пополнить: " + bad_pair);
		    }
	    	c.commit();
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
	}
	
	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		listModel = new DefaultListModel<String>();
		askAgent = new AskAgent(this);
		
		setBounds(100, 100, 363, 356);
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
		
		JScrollPane scrollPane = new JScrollPane();
		
		JButton button = new JButton("\u041E\u041A");
		button.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				setVisible(false);
			}
		});
		
		JButton btnNewButton = new JButton("\u041E\u0431\u043D\u043E\u0432\u0438\u0442\u044C");
		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				refresh();
			}
		});
		JButton btnNewButton_1 = new JButton("\u0421\u043C\u0435\u043D\u0438\u0442\u044C \u0430\u0433\u0435\u043D\u0442\u0430");
		btnNewButton_1.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				askAgent.setVisible(true);
			}
		});
		
		JButton btnNewButton_2 = new JButton("\u041F\u043E\u043F\u043E\u043B\u043D\u0438\u0442\u044C \u0441\u043A\u043B\u0430\u0434\u044B");
		btnNewButton_2.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				update();
			}
		});
		
		label = new JLabel("\u0412\u044B\u0431\u0440\u0430\u043D\u043D\u044B\u0439 \u0430\u0433\u0435\u043D\u0442");
		
		JLabel label_1 = new JLabel("\u0421\u043A\u043B\u0430\u0434 | \u041E\u0442\u0441\u0443\u0442\u0441\u0442\u0432\u0443\u044E\u0449\u0438\u0439 \u0442\u043E\u0432\u0430\u0440");
		GroupLayout groupLayout = new GroupLayout(getContentPane());
		groupLayout.setHorizontalGroup(
			groupLayout.createParallelGroup(Alignment.LEADING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addGroup(groupLayout.createParallelGroup(Alignment.LEADING)
						.addComponent(label_1)
						.addComponent(scrollPane, Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 327, Short.MAX_VALUE)
						.addGroup(Alignment.TRAILING, groupLayout.createSequentialGroup()
							.addComponent(btnNewButton, GroupLayout.DEFAULT_SIZE, 83, Short.MAX_VALUE)
							.addPreferredGap(ComponentPlacement.RELATED)
							.addComponent(btnNewButton_1, GroupLayout.DEFAULT_SIZE, 185, Short.MAX_VALUE)
							.addPreferredGap(ComponentPlacement.RELATED)
							.addComponent(button, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
						.addComponent(label)
						.addComponent(btnNewButton_2, GroupLayout.DEFAULT_SIZE, 327, Short.MAX_VALUE))
					.addContainerGap())
		);
		groupLayout.setVerticalGroup(
			groupLayout.createParallelGroup(Alignment.TRAILING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addComponent(label_1)
					.addGap(3)
					.addComponent(scrollPane, GroupLayout.DEFAULT_SIZE, 203, Short.MAX_VALUE)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addComponent(label)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addComponent(btnNewButton_2)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addGroup(groupLayout.createParallelGroup(Alignment.BASELINE)
						.addComponent(btnNewButton)
						.addComponent(btnNewButton_1)
						.addComponent(button))
					.addGap(10))
		);
		
		JList<String> list = new JList<String>(listModel);
		scrollPane.setViewportView(list);
		getContentPane().setLayout(groupLayout);
	}
}
