import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Arrays;

  public class Token {

public static void main(String arg[]) throws IOException {

    ProcessBuilder pb = new ProcessBuilder(
    		"\"C:\\Users\\daniel\\curl-7.54.1-win64-mingw\\bin\\curl\"",
    		"-X", "\"POST\"", 
    		"\"https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token\"",
    		"\\", "-H", "\"Content-Type: application/x-www-form-urlencoded; charset=utf-8\"",
    		"\\", "--data-urlencode", "client_id=api-stag", "\\", "--data-urlencode",
    		"\"username=cpf-07-0185-0132@stag.comprobanteselectronicos.go.cr\"",
    		"\\", "--data-urlencode","\"password=y@/K{_[Ih&7>ao==I?Iw\"",
    		"\\", "--data-urlencode", "\"grant_type=password\"");

    //pb.directory(new File("/home/your_user_name/Pictures"));
    //pb.redirectErrorStream(true);
    Process p = pb.start();
    InputStream is = p.getInputStream();

    //FileOutputStream outputStream = new FileOutputStream("/home/your_user_name/Pictures/simpson_download.jpg");

    BufferedInputStream bis = new BufferedInputStream(is);
    BufferedReader reader = new BufferedReader(new InputStreamReader(bis));
    StringBuilder out = new StringBuilder();
    String line;
    while ((line = reader.readLine()) != null) {
        out.append(line);
    }
    System.out.println("Acceso autorizado factura electronica");
    System.out.println(out.toString());   //Prints the string content read from input stream
    reader.close();
    //https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token\
    
    
   
}
 }