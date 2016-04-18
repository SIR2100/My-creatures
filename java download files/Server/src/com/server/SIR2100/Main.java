package com.server.SIR2100;

    import java.io.*;
    import java.net.*;
    import java.util.Calendar;
    import java.util.Vector;
    import static java.util.Calendar.*;

public class Main {
    public static void main(String[] args) {
        ServerThread.writeLog("Initialisation...");
        ConnectListener connect =new ConnectListener();
        connect.setDaemon(true);
        connect.start();
        BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));
        while(true){
            try {
                String s = inFromUser.readLine();
                if (s.equals("help")) {
                    ServerThread.writeLog(
                            "------------------------------- \n       " +
                            "help: for help \n       " +
                            "stop: stop server \n" +
                            "-------------------------------");
                }
                else if (s.equals("stop")) {
                    connect.fastStop();
                    System.exit(0);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}

class ConnectListener extends Thread{
    private Vector<ServerThread> threads = new Vector<>();
    public void run() {
        try {
            ServerSocket serve = new ServerSocket(1002);
            ServerThread.writeLog("Server is working now!");
            ServerThread.writeLog("Type 'help' for help");
            while (true) {
                // Ожидание клиента
                Socket sock = serve.accept();
                ServerThread.writeLog(sock.getInetAddress().getHostName() + " connected");
                // Создание отдельного потока для обмена данными с соединившимся клиентом
                ServerThread server = new ServerThread(sock);
                threads.add(server);
                // Запуск потока
                server.start();
            }
        } catch (IOException e) {
            ServerThread.writeLog(e.toString());
        }
    }

    // Остановка сервера
    void fastStop(){
        int all = 0;
        int inactive = 0;
        for(ServerThread th:threads){
            all++;
            if(th.isAlive()) {
                th.disconnect();
                if(!th.isAlive())inactive++;
            }
        }
        ServerThread.writeLog("Result: " + inactive + "/" + all + " stopped");
    }
}

class ServerThread extends Thread {
    private PrintStream os;// Исходящий поток
    private BufferedReader is;// Входящий поток
    private InetAddress ip;// Адрес клиента
    private Boolean doExit = false;

    ServerThread(Socket s) throws IOException {
        os = new PrintStream(s.getOutputStream());
        is = new BufferedReader(new InputStreamReader(s.getInputStream()));
        ip = s.getInetAddress();
    }

    // Получение команд от клиента
    public void run() {
        String str;
        try {
            while ((!doExit) && (str = is.readLine()) != null)  {

                //Принимаем ответ от клиента
                writeLog(ip.getHostName() + ": " + str);
                switch (str) {
                    case "stop": // Отключаем клиента
                        disconnect();
                        doExit = true;
                        break;
                    case "get": // Получить список файлов сервера
                        os.println(getFiles());
                        break;
                    case "help": // Получить помощь от сервера
                        String help = "Get files: get||"
                                + "Download file: download||"
                                + "Disconnect from server: stop";
                        os.println(help);
                        break;
                    case "download": // Загрузка файла
                        os.println("Filename?");
                        download(is.readLine());
                        break;
                    default:
                        writeLog(ip.getHostName() + ": " + str + " - incorrect command" );
                        os.println("Incorrect command. Type help.");
                        break;
                }
            }
        } catch (IOException e) {
            //System.out.println("Disconnect");
        }
    }

    //Функция передачи файла на клиент
    private void download(String filename) {
        writeLog(ip.getHostName()+ ": asking for download '" + filename + "';");
        try
        {
            File file = new File("C:\\Temp\\" + filename);
            if (file.exists()) {
                FileInputStream in = new FileInputStream(file);
                byte[] mybytearray = new byte[(int) file.length()];
                getfilestat(filename);
                int counter;
                //os.write(mybytearray.length);
                while((counter = in.read(mybytearray)) != -1){
                    os.write(mybytearray, 0, counter);
                }
                os.flush();
                writeLog(ip.getHostName() + " - Files sent successfully!" );
            } else {os.write(0);}

        } catch (Exception e){
            writeLog(ip.getHostName() + ": Error - " + e);
        }
    }

    // Получение списка файлов
    private String getFiles() {
        File []fList;
        File F = new File("C:\\Temp");
        String res = "";

        fList = F.listFiles();

        assert fList != null;
        for(int i = 0; i<fList.length; i++)
            if (fList[i].isFile()) {
                res += (fList[i].getName());
                if (i + 2 < fList.length) {
                    res += ", ";
                }
            }
        return res;
    }

    //Отключение клиента
    public void disconnect() {
        try {
            os.close();
            is.close();
            this.interrupt();
            writeLog(ip.getHostName() + ": disconnected");
        } catch (IOException e) {
            writeLog(ip.getHostName() + ": Error - cannot disconnect");
            System.err.println(e.toString());
        }
    }

    // Функция логирования
    static String writeLog(String stat) {
        Calendar c= getInstance();

        int month=c.get(MONTH)+1;
        int day=c.get(DAY_OF_MONTH);
        int hour=c.get(HOUR_OF_DAY);
        int min=c.get(MINUTE);
        int sec=c.get(SECOND);
        byte[] buffer;
        File logFile = new File("C:\\" + "log.txt");
        try
        {
            FileOutputStream fos=new FileOutputStream(logFile, true);
            //Если log файла не существует - создаем его
            if (!logFile.exists()) {
                try {
                    if (logFile.createNewFile()) {
                        writeLog("Log file создан!");
                    }
                } catch (IOException ex) {
                    System.err.println("Ошибка создания log файла!");
                }
            }
            String outString =month + "." + day + " " + hour + "." + min + "." + sec + "||" + stat + "\r\n";

            buffer = (outString).getBytes();
            System.out.print(outString);
            fos.write(buffer, 0, buffer.length);
        } catch (Exception e){
            StringWriter errors = new StringWriter();
            e.printStackTrace(new PrintWriter(errors));
            return errors.toString();
        }
        return stat;
    }

    //Функция записи кол-ва скачиваний файлов
    private String getfilestat(String filename) {
        try {
            boolean isFirst = false;
            boolean found = false;
            File file = new File("C:/data.txt");
            //Если файла не существует - создаем его
            if (!file.exists()) {
                try{
                    if (file.createNewFile()) {
                        isFirst = true;
                        writeLog("Файл статистики создан!");
                    }
                }catch (IOException e) {
                    writeLog("Ошибка создания файла статистики");
                }
            }
            FileInputStream defile = new FileInputStream(file);
            byte[] content = new byte[defile.available()];
            defile.close();
            String[] lines = new String(content, "UTF-8").split("\n");
            int i = 1;
            //Разбиваем файла на строки, проверяем вхождения искомого названия файла
            for (String line : lines) {
                //Если вхождение найдено - обновляем строку
                if (line.contains(filename)) {
                    String[] words = line.split(" ");
                    lines[i-1] = words[0] + " " + (Integer.parseInt(words[1].replaceAll("\r", ""))+ 1) + "\r";
                    writeLog("log file was edited: " + lines[i-1]);
                    found = true;
                }
                i++;
            }
            //Записываем в файл обновленные данные
            try(FileOutputStream fos=new FileOutputStream(file)){
                i = 1;
                byte[] buffer;
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
                    writeLog("log file was edited: " + filename + " 1");
                    fos.write(buffer, 0, buffer.length);
                }
            }catch (Exception e) {
                StringWriter errors = new StringWriter();
                e.printStackTrace(new PrintWriter(errors));
                return errors.toString();
            }

        }catch (IOException e) {
            StringWriter errors = new StringWriter();
            e.printStackTrace(new PrintWriter(errors));
            return errors.toString();
        }
        return filename;
    }

}