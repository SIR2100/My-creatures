import java.io.FileInputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Properties;

import javax.swing.DefaultListModel;
import javax.swing.JFrame;
import javax.swing.GroupLayout;
import javax.swing.GroupLayout.Alignment;
import javax.swing.JButton;
import javax.swing.LayoutStyle.ComponentPlacement;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

import javax.swing.JList;
import javax.swing.JScrollPane;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;


@SuppressWarnings("serial")
public class Journal extends JFrame{


	/**
	 * Create the application.
	 */
	public Journal() {
		initialize();
	}
	
	private DefaultListModel<String> listModel;

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
	    	rs = s.executeQuery("select * from TOVAR_MOVING");
	    	while (rs.next()) {
	    	 //String rightString= new String(badString.getBytes("windows-1251"),"utf-8");
	    		String inform = new String(rs.getString("INFORM").getBytes(),"utf-8").trim();
	    		String ddate = new String(rs.getDate("DDATE").toString().getBytes(),"utf-8").trim();
	    		listModel.addElement(ddate +" | "+ inform);
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
	
	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		setBounds(100, 100, 450, 300);
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
		listModel = new DefaultListModel<String>();

		
		JButton btnNewButton = new JButton("\u041E\u041A");
		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				setVisible(false);
			}
		});
		addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent e) {
				refresh();
			}
		});
		JButton button = new JButton("\u041E\u0431\u043D\u043E\u0432\u0438\u0442\u044C");
		button.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				refresh();
			}
		});
		
		JScrollPane scrollPane = new JScrollPane();
		GroupLayout groupLayout = new GroupLayout(getContentPane());
		groupLayout.setHorizontalGroup(
			groupLayout.createParallelGroup(Alignment.LEADING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addGroup(groupLayout.createParallelGroup(Alignment.LEADING)
						.addComponent(scrollPane, GroupLayout.DEFAULT_SIZE, 414, Short.MAX_VALUE)
						.addGroup(groupLayout.createSequentialGroup()
							.addComponent(button)
							.addPreferredGap(ComponentPlacement.RELATED, 284, Short.MAX_VALUE)
							.addComponent(btnNewButton)))
					.addContainerGap())
		);
		groupLayout.setVerticalGroup(
			groupLayout.createParallelGroup(Alignment.TRAILING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addComponent(scrollPane, GroupLayout.DEFAULT_SIZE, 211, Short.MAX_VALUE)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addGroup(groupLayout.createParallelGroup(Alignment.BASELINE)
						.addComponent(btnNewButton)
						.addComponent(button))
					.addContainerGap())
		);
		
		JList<String> list = new JList<String>(listModel);
		scrollPane.setViewportView(list);
		getContentPane().setLayout(groupLayout);
	}
}
