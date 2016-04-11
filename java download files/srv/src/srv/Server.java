package srv;
import java.io.*;
import java.net.*;

public class Server {
	public static void main(String[] args) throws IOException {
		ServerSocket serv = null;
		try {
			serv = new ServerSocket(6789);
			System.out.println("Server is working now");
			while (true) {
				// Ожидание клиента
				Socket sock = serv.accept();
				System.out.println(sock.getInetAddress().getHostName() + " connected");
				// Создание отдельного потока для обмена данными с соединившимся клиентом
				ServerThread server = new ServerThread(sock);
				// Запуск потока
				server.start();
				
			}
		} catch (IOException e) {
			System.err.println(e);
		} finally {
			//serv.close();
		}
	}
	
}
class ServerThread extends Thread {
	private PrintStream os;// Исходящий поток
	private BufferedReader is;// Входящий поток
	private InetAddress ip;// Адрес клиента
	private Boolean doExit = false;
	private OutputStream tos;
	
	public ServerThread(Socket s) throws IOException {
		os = new PrintStream(s.getOutputStream());
		is = new BufferedReader(new InputStreamReader(s.getInputStream()));
		ip = s.getInetAddress();
		tos = s.getOutputStream();
	}
	
	public void run() {
		String str;
		try {
			while ((!doExit) && ((str = is.readLine()) != null)) {
				System.out.println(ip.getHostName()+ ": " + str);
				switch(str) {
			    case "stop": 
					disconnect();
					doExit = true;
					break;
			    case "get": 
			    	getFiles();
					break;
			    case "help": 
			    	String help = "Get files: get||"
							+ "Download file: download||"
							+ "Disconnect from server: stop";
					os.println(help);
					break;	
			    case "download": 
					os.println("Filename?");
					download(is.readLine());
					os.println("Files sent successfully!");
					break;	
				default: 
					os.println("Incorrect command. Type help.");
				    break;
				}
			}
		} catch (IOException e) {
		//System.out.println("Disconnect");
		} finally 
		{
			//disconnect();
		}
	}

	public void download(String filename) {
		System.out.println(ip.getHostName()+ ": asking for download '" + filename + "';");
		try
		{
			File file = new File("C:\\Temp\\" + filename);
		    FileInputStream in = new FileInputStream(file);
			byte[] mybytearray = new byte[(int) file.length()];
			getfilestat(filename);
			if (mybytearray.length <= Integer.MAX_VALUE) {
			    int counter;
			    //os.write(mybytearray.length);
			    while((counter = in.read(mybytearray)) != -1){
			    	os.write(mybytearray, 0, counter);
				} 
			    os.flush(); 
            }else{
            	System.out.println("File is too large.");
                os.println("File is too large.");
            }
			
		    
		} catch (Exception e){
			System.err.println(e);
		} finally {
			//os.println();
		}
	}
	
	public void getFiles() {
		File []fList;        
		File F = new File("C:\\Temp");
		String res = ""; 
		        
		fList = F.listFiles();
		                
		for(int i=0; i<fList.length; i++)           
		{
		     if(fList[i].isFile()){
		         res += (fList[i].getName());
		         if(i+2<fList.length) {res += ", ";} else {res += ".";} 
		     }
		}
		os.println(res);
	}
		
	public void disconnect() {
		try {
			System.out.println(ip.getHostName() + " disconnected");
			os.close();
			is.close();
		} catch (IOException e) {
			System.out.println("Cannot disconnect: " + ip.getHostName());
		}
	}
	
	public void getfilestat(String filename) {
		try {
		    FileInputStream dfile = new FileInputStream(new File("C:/data.txt"));
		    byte[] content = new byte[dfile.available()];
		    dfile.read(content);
		    dfile.close();
		    String[] lines = new String(content, "UTF_8").split("\n"); // кодировку указать нужную
		    int i = 1;
		    for (String line : lines) {
		        String[] words = line.split(" ");
		        int j = 1;
		        for (String word : words) {
		            if (word.equalsIgnoreCase(filename)) {
		                System.out.println("Найдено");
		            }
		            j++;
		        }
		        i++;
		    }
		    //Если не найдено - создаем новое вхождение
		    
		}catch (IOException e) {
			System.out.println(e);
			try{
				File newFile = new File("C:/data.txt");
				if (newFile.createNewFile()) {
					System.out.println("Файл статистики создан!");
			}
				}catch (IOException ex) {
				System.err.println("Ошибка создания файла!");;
			}
		}
	}
}