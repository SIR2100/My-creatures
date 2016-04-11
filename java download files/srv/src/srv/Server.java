package srv;
import java.io.*;
import java.net.*;
import java.util.ArrayList;

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
					os.println(getFiles());
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
			if (file.exists()) {
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
					os.println("Files sent successfully!");
	            }else{
	            	System.out.println("File is too large.");
	                os.println("File is too large.");
	            }
			} else {os.write(0);}
		    
		} catch (Exception e){
			System.err.println(e);
		} finally {
			//os.println();
		}
	}
	
	public String getFiles() {
		File []fList;        
		File F = new File("C:\\Temp");
		String res = ""; 
		        
		fList = F.listFiles();
		                
		for(int i=0; i<fList.length; i++)           
		{
		     if(fList[i].isFile()){
		         res += (fList[i].getName());
		         if(i+2<fList.length) {res += ", ";}
		     }
		}
		return res;
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
			boolean isFirst = false;
			boolean found = false;
			File file = new File("C:/data.txt");
			//Если файла не существует - создаем его
			if (!file.exists()) {
				try{
					if (file.createNewFile()) {
						isFirst = true;
						System.out.println("Файл статистики создан!");
				}
					}catch (IOException ex) {
					System.err.println("Ошибка создания файла!");;
				}
			}
		    FileInputStream dfile = new FileInputStream(file);
		    byte[] content = new byte[dfile.available()];
		    dfile.read(content);
		    dfile.close();
		    String[] lines = new String(content, "UTF-8").split("\n");
		    int i = 1;
		    //Разбиваем файла на строки, проверяем вхождения искомого названия файла
		    for (String line : lines) {
		    	//Если вхождение найдено - обновляем строку
		    	if (line.contains(filename)) {
			        String[] words = line.split(" ");
		    		lines[i-1] = words[0] + " " + (Integer.parseInt(words[1].replaceAll("\r", ""))+ 1) + "\r";
		    		System.out.print("log file was edited: " + lines[i-1]);
		    		found = true;
		    	}
		        i++;
		    }
		    //Записываем в файл обновленные данные
    		try(FileOutputStream fos=new FileOutputStream(file)){
    			i = 1;
    			byte[] buffer = null;
    			for (String newline : lines) {
    				if (i == lines.length) {
		    			buffer = (newline).getBytes();
    				}
    				else
    				{
    					buffer = (newline + "\n").getBytes();
    				}
    				fos.write(buffer, 0, buffer.length);
    				i++;
    			}
    			//Если вхождения с искомым названием скачиваемого файла не было, создаем вхождение
    		    if (!found) {
    		    	if (isFirst) 
    		    	{
    		    		buffer = (filename + " 1").getBytes();
    		    	}
    		    	else
    		    	{
    		    		buffer = ("\r\n" + filename + " 1").getBytes();
    		    	}
    		    	System.out.print("log file was edited: " + filename + " 1");
	    			fos.write(buffer, 0, buffer.length);
    		    }
    		}catch (Exception e) {System.err.println(e);}
		    
		}catch (IOException e) {System.out.println(e);}
	}
	public static String[] divide(String s) {
        ArrayList<String> tmp = new ArrayList<String>();
        int i = 0;
        boolean f = false;

        for (int j = 0; j < s.length(); j++) {
            if (s.charAt(j) == ' ') {
                if (j > i) {
                    tmp.add(s.substring(i, j));
                }
                i = j + 1;
            }
        }
        if (i < s.length()) {
            tmp.add(s.substring(i));
        }
        return tmp.toArray(new String[tmp.size()]);
    }
}