package com.client.SIR2100;

import java.io.*;
import java.net.*;

public class Main {
    public static void main(String argv[]) throws Exception
    {
        String sentence;
        Boolean doIt = true;
        Socket clientSocket = null;
        boolean isReadyToDownload = false;


        BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));

        try {
            clientSocket = new Socket("127.0.0.1", 1002);
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
                DataInputStream is = new DataInputStream(clientSocket.getInputStream());
                FileOutputStream fos;
                sentence = inFromUser.readLine();
                os.writeBytes(sentence + '\n');
                if (isReadyToDownload) try {
                    fos = new FileOutputStream("C:/" + sentence);
                    byte[] buffer = new byte[64 * 1024];
                    int count;
                    boolean firstok = false;

                    while (-1 != (count = is.read(buffer))) {
                        fos.write(buffer, 0, count);
                        if (count < 65536) {
                            if ((count == 1) && (!firstok)) {
                                //is.read(buffer);
                                fileDoesNotConsist = true;
                                System.out.println("Файл не существует");
                            }
                            break;
                        }
                        firstok = true;
                    }
                    //System.out.println("File downloaded!");
                } catch (Exception e) {
                    System.out.println(e.toString());
                } finally {
                    isReadyToDownload = false;
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
                    System.out.printf("SERVER:%s%n", is.readLine());
                }
            } catch (Exception e) {
                System.err.println("Disconnected");
            }
        }
    }
}
