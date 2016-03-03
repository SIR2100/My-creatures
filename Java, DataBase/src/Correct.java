import javax.swing.JFrame;

import java.awt.BorderLayout;

import javax.swing.JSplitPane;
import javax.swing.JPanel;
import javax.swing.GroupLayout;
import javax.swing.GroupLayout.Alignment;
import javax.swing.LayoutStyle.ComponentPlacement;

import javax.swing.DefaultListModel;
import javax.swing.JScrollPane;
import javax.swing.JList;
import javax.swing.JButton;

import java.io.FileInputStream;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;

import javax.swing.event.ListSelectionListener;
import javax.swing.event.ListSelectionEvent;
import javax.swing.ListSelectionModel;

import javax.swing.JTextPane;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

@SuppressWarnings("serial")
public class Correct extends JFrame{

	/**
	 * Create the application.
	 */
	public Correct() {
		initialize();
	}

	private DefaultListModel<String> listModel;
	private List<String> Msgs;
	private JTextPane textPane;
	
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
	    	rs = s.executeQuery("select * from CORRECT");
	    	while (rs.next()) {
	    	 //String rightString= new String(badString.getBytes("windows-1251"),"utf-8");
	    		String Msg = "";
	    		String naimen = new String(rs.getString("NAIMEN").getBytes(),"utf-8").trim();
	    		String nomenclature = new String(rs.getString("NOMENCLATURE").getBytes(),"utf-8").trim();
	    		double right_value = rs.getDouble("RIGHT_VALUE");
	    		double false_value = rs.getDouble("FALSE_VALUE");
	    		if (!rs.wasNull())
	    			Msg = "Несоответсвие значения для склада " + naimen + " и товара " + nomenclature + "\n\nУказанное значение: " + false_value + "\n\nПравильное значение: " + right_value;
	    		else
	    			Msg = "Нет записи для склада " + naimen + " и товара " + nomenclature + "\n\nВычисленное значение: " + right_value;
	    		Msgs.add(Msg);
	    		listModel.addElement(naimen +" | "+ nomenclature);
	    	}
	    	if (listModel.isEmpty())
	    		textPane.setText("Несоответствий не выявлено.");
	    	else
		    	textPane.setText("");

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
	    CallableStatement s = null;
	    ResultSet rs = null;
	
	    try{
	    	Class.forName(driverName);
	    	c = DriverManager.getConnection(databaseURL, props);
	    	c.getMetaData();
	    	c.setAutoCommit(false);
	    	s = c.prepareCall( " { call UPDATE_CORRECTION }"); 
	    	s.executeUpdate(); 
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
		Msgs = new ArrayList<String>();
		this.addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent arg0) {
				refresh();
			}
		});
		this.setTitle("\u041A\u043E\u0440\u0440\u0435\u043A\u0442\u0438\u0440\u043E\u0432\u043A\u0430");
		this.setBounds(100, 100, 385, 392);
		this.setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
		
		JSplitPane splitPane = new JSplitPane();
		splitPane.setOneTouchExpandable(true);
		splitPane.setResizeWeight(1.0);
		splitPane.setOrientation(JSplitPane.VERTICAL_SPLIT);
		splitPane.setDividerLocation(((getHeight()-20) / 11)*6);
		
		JPanel panel_1 = new JPanel();
		splitPane.setLeftComponent(panel_1);
		panel_1.setLayout(new BorderLayout(0, 0));
		
		JScrollPane scrollPane = new JScrollPane();
		panel_1.add(scrollPane);
		
		JList<String> list = new JList<String>(listModel);
		list.setSelectionMode(ListSelectionModel.SINGLE_INTERVAL_SELECTION);
		list.addListSelectionListener(new ListSelectionListener() {
			public void valueChanged(ListSelectionEvent arg0) {
				textPane.setText(Msgs.get(arg0.getFirstIndex()));
			}
		});
		scrollPane.setViewportView(list);
		
		JPanel panel = new JPanel();
		splitPane.setRightComponent(panel);
		panel.setLayout(new BorderLayout(0, 0));
		
		JScrollPane scrollPane_1 = new JScrollPane();
		panel.add(scrollPane_1, BorderLayout.CENTER);
		
		textPane = new JTextPane();
		textPane.setEditable(false);
		scrollPane_1.setViewportView(textPane);
		
		JButton btnNewButton = new JButton("\u041A\u043E\u0440\u0440\u0435\u043A\u0442\u0438\u0440\u043E\u0432\u0430\u0442\u044C \u0432\u0441\u0451");
		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				update();
				refresh();
			}
		});
		
		JButton button = new JButton("\u041E\u0431\u043D\u043E\u0432\u0438\u0442\u044C");
		button.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				refresh();
			}
		});
		
		JButton button_1 = new JButton("\u041E\u041A");
		button_1.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				setVisible(false);
			}
		});
		GroupLayout groupLayout = new GroupLayout(this.getContentPane());
		groupLayout.setHorizontalGroup(
			groupLayout.createParallelGroup(Alignment.TRAILING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addGroup(groupLayout.createParallelGroup(Alignment.LEADING)
						.addGroup(groupLayout.createSequentialGroup()
							.addComponent(button)
							.addPreferredGap(ComponentPlacement.RELATED)
							.addComponent(btnNewButton, GroupLayout.DEFAULT_SIZE, 207, Short.MAX_VALUE)
							.addPreferredGap(ComponentPlacement.RELATED)
							.addComponent(button_1))
						.addComponent(splitPane, GroupLayout.DEFAULT_SIZE, 349, Short.MAX_VALUE))
					.addContainerGap())
		);
		groupLayout.setVerticalGroup(
			groupLayout.createParallelGroup(Alignment.TRAILING)
				.addGroup(groupLayout.createSequentialGroup()
					.addContainerGap()
					.addComponent(splitPane, GroupLayout.DEFAULT_SIZE, 303, Short.MAX_VALUE)
					.addPreferredGap(ComponentPlacement.RELATED)
					.addGroup(groupLayout.createParallelGroup(Alignment.BASELINE)
						.addComponent(button)
						.addComponent(btnNewButton)
						.addComponent(button_1))
					.addContainerGap())
		);
		this.getContentPane().setLayout(groupLayout);
	}
}
