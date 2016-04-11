package client;
import java.io.*;
import java.net.*;

public class client {
	public static void main(String argv[]) throws Exception
	{		
		String sentence;
		Boolean doIt = true;
		Socket clientSocket = null;
		boolean isReadyToDownload = false;

		
		BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));

		try {
			clientSocket = new Socket("127.0.0.1", 6789);
			System.out.println("Connected");
		} catch (Exception e) {
			System.out.println("Ip incorrect");
			doIt = false;
		}
		
		while (doIt) {
			boolean fileDoesNotConsist = false;
			try {
				System.out.print("You:");
				DataOutputStream os = new DataOutputStream(clientSocket.getOutputStream());
				PrintWriter los = new PrintWriter(clientSocket.getOutputStream());
				DataInputStream is = new DataInputStream(clientSocket.getInputStream());
				FileOutputStream fos = null;
				sentence = inFromUser.readLine();
				os.writeBytes(sentence + '\n');
				if (isReadyToDownload) {
					try
					{
				            fos=new FileOutputStream("C:/"+sentence);
				            byte[] buffer = new byte[64*1024];
				            int count=0;
				            boolean firstok = false;
				            
				            while((count = is.read(buffer)) != -1){
				                fos.write(buffer, 0, count);
				                if (count < 65536) 
				                {
				                	if ((count == 1) && (!firstok))
				                	{
				                		//is.read(buffer);
				                		fileDoesNotConsist = true;
				                		System.out.println("Файл не существует");
				                	}
				                	break;
				                }
				                firstok = true;
				            }
				            //System.out.println("File downloaded!");
				            if (firstok)
				            los = new PrintWriter(clientSocket.getOutputStream(),true);
					}
					catch (Exception e) {
						System.out.println(e);
					} finally {
						isReadyToDownload = false;
					}
				}
				switch(sentence) {
				case "stop": 
					clientSocket.close();
					doIt = false;
					break;	
				case "download": 
					isReadyToDownload = true;
					break;
				default: 
				    //break;
				}
				if (!fileDoesNotConsist) {
					System.out.println("SERVER:"  + is.readLine());
				}
			} catch (Exception e) {
				System.err.println("Disconnected");
			}
		}
	}
}
