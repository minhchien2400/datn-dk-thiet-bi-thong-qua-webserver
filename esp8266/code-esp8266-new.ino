#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <SoftwareSerial.h>
#include "DHT.h"
#include <IRremoteESP8266.h>
#include <IRsend.h>
#include <ir_Panasonic.h>

// Khởi tạo wifi muốn kết nối
const char* ssid = "iPhone"; //KIIO tên wifi
const char* password = "112345678"; // password wifi

WiFiClient client; // khởi tạo 1 đối tượng client
HTTPClient http;

// setup dht11
const int DHTTYPE = DHT11;
const int DHTPIN = D4;
const uint16_t kIrLed = 4; // chan D2

DHT dht(DHTPIN, DHTTYPE);
IRPanasonicAc ac(kIrLed);
IRsend irsend(kIrLed);


// setup led
int led1 = D1;
int led2red = D5;
int led2green = D6;
int led2blue = D7;
//String checkkey = "";

//setup
int sensor_fire = D3;

const String httpUrl = "http://172.20.10.5:5248/";

void setup() {
      pinMode(led1, OUTPUT);   
      pinMode(led2red, OUTPUT);   
      pinMode(led2green, OUTPUT);   
      pinMode(led2blue, OUTPUT);
      pinMode(sensor_fire, INPUT);     
      ac.begin();
      irsend.begin(); 
  Serial.begin(115200);
   dht.begin();
  delay(1000);
  Serial.println("\n\nWelcome Esp8266-Webserver\n");
  setup_wifi();
}

// hàm setup kết nối
void setup_wifi() {
  delay(10);
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}


void loop() {
  ac.setModel(kPanasonicRkr);
  setTempAir();
  firealarm();
  temphum();
  String oldKey = remote();
  String url = httpUrl+"getkeyremote/1";
  String newKey = getApi(url);
  while(oldKey == newKey)
  {
    firealarm();
    temphum();
    setTempAir();
    newKey = getApi(url);
  }
  delay(100);
}

// get api
String getApi(String requestUrl){
  //Check WiFi connection status
  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("\n\nPerforming HTTP GET Request\n");

    // HTTP Details

    http.begin(client, requestUrl);
    //    http.setAuthorization("Basic token");
    //    http.setAuthorization("Bearer token");

    // Send HTTP GET request
    int httpResponseCode = http.GET();
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);

    if (httpResponseCode == HTTP_CODE_OK) {
      String payload = http.getString();
      Serial.println("Response payload: " + payload);

      DynamicJsonDocument doc(1024);
      deserializeJson(doc, payload);
      JsonObject obj = doc.as<JsonObject>();

      String value = obj[String("response")];
      Serial.println("\nresponse is : " + value);

      return payload;
    }
    else {
      Serial.print("Error code: ");
      Serial.println(httpResponseCode);
    }
    // Free resources
    http.end();

  } else {
    Serial.println("WiFi Disconnected");
  }
      return "action failed";
}


// api remote
String remote()
{
  ac.setModel(kPanasonicRkr);
  String url2 = httpUrl+"getkeyremote/1";
  String keyRemode = getApi(url2);
  // den 1
      if (keyRemode == "1" ) {
        digitalWrite(led1, HIGH);
        Serial.println("nLED1 ON");
        delay(100);
        return "1";
      }
      if (keyRemode == "2" ) {
        digitalWrite(led1, LOW);
        Serial.println("\nLED1 OFF");
        delay(100);
        return "2";
      }
  // den 2
      if (keyRemode == "3" ) {
        digitalWrite(led2red, HIGH);
        digitalWrite(led2green, LOW);
        digitalWrite(led2blue, LOW);
        Serial.println("\nLED2 ON-RED");
        delay(100);
        return "3";
      }
      if (keyRemode == "4" ) {
        digitalWrite(led2red, LOW);
        digitalWrite(led2green, LOW);
        digitalWrite(led2blue, LOW);
        Serial.println("\nLED2 ON -RED");
        delay(100);
        return "4";
      }
      if (keyRemode == "18" ) {
        digitalWrite(led2red, LOW);
        digitalWrite(led2green, HIGH);
        digitalWrite(led2blue, LOW);
        Serial.println("\nLED2 ON-GREEN");
        delay(100);
        return "18";
      }
      if (keyRemode == "19" ) {
        digitalWrite(led2red, LOW);
        digitalWrite(led2green, LOW);
        digitalWrite(led2blue, HIGH);
        Serial.println("\nLED2 ON-BLUE");
        delay(100);
        return "19";
      }

      // loa
      if (keyRemode == "20" ) {
        irsend.sendNEC(0xFFA25D ,32);
        Serial.println("\nLoa ON");
        delay(100);
        return "20";
      }
      if (keyRemode == "21" ) {
        irsend.sendNEC(0xFFA25D ,32);
        Serial.println("\nLoa OFF");
        delay(100);
        return "21";
      }
      if (keyRemode == "11" ) {
        irsend.sendNEC(0xFF22DD ,32);
        Serial.println("\nLoa Play");
        delay(100);
        return "11";
      }
      if (keyRemode == "12" ) {
        irsend.sendNEC(0xFF22DD ,32);
        Serial.println("\nLoa Pause");
        delay(100);
        return "12";
      }
      if (keyRemode == "15" ) {
        irsend.sendNEC(0xFFE21D ,32);
        Serial.println("\nLoa Mute");
        delay(100);
        return "15";
      }
      if (keyRemode == "22" ) {
        irsend.sendNEC(0xFFE21D ,32);
        Serial.println("\nLoa Not Mute");
        delay(100);
        return "22";
      }
      if (keyRemode == "13" ) {
        irsend.sendNEC(0xBF02FD ,32);
        Serial.println("\nBack music");
        delay(100);
        return "13";
      }
      if (keyRemode == "17" ) {
        irsend.sendNEC(0xBF02FD ,32);
        Serial.println("\nBack music");
        delay(100);
        return "17";
      }
      if (keyRemode == "16" ) {
        irsend.sendNEC(0xFFC23D ,32);
        Serial.println("\nNextmusic");
        delay(100);
        return "16";
      }
      if (keyRemode == "14" ) {
        irsend.sendNEC(0xFFC23D ,32);
        Serial.println("\nNextmusic");
        delay(100);
        return "14";
      }

      //dieu hoa
      if (keyRemode == "5" ) {
        ac.on();
        ac.send();
        Serial.println("\nAir condition ON");
        delay(100);
        return "5";
      }
      if (keyRemode == "6" ) {
        ac.off();
        ac.send();
        Serial.println("\nAir condition Off");
        delay(100);
        return "6";
      }
      if (keyRemode == "7" ) {
        ac.setMode(kPanasonicAcAuto);
        ac.send();
        Serial.println("\nMode auto");
        delay(100);
        return "7";
      }
      if (keyRemode == "8" ) {
        ac.setMode(kPanasonicAcHeat);
        ac.send();
        Serial.println("\nMode heat");
        delay(100);
        return "8";
      }
      if (keyRemode == "9" ) {
        ac.setMode(kPanasonicAcCool);
        ac.send();
        Serial.println("\nMode cool");
        delay(100);
        return "9";
      }
      if (keyRemode == "10" ) {
        ac.setMode(kPanasonicAcDry);
        ac.send();
        Serial.println("\nMode Dry");
        delay(100);
        return "10";
      }
      if (keyRemode == "23" ) {
        ac.setMode(kPanasonicAcFan);
        ac.send();
        Serial.println("\nMode FAN");
        delay(100);
        return "23";
      }
      if (keyRemode == "24" ) {
        ac.setFan(kPanasonicAcFanAuto);
        ac.send();
        Serial.println("\nSpeed FAN: AUTO");
        delay(100);
        return "24";
      }
      if (keyRemode == "25" ) {
        ac.setFan(kPanasonicAcFanMin);
        ac.send();
        Serial.println("\nSpeed FAN: 1");
        delay(100);
        return "25";
      }
      if (keyRemode == "26" ) {
        ac.setFan(kPanasonicAcFanLow);
        ac.send();
        Serial.println("\nSpeed FAN: 2");
        delay(100);
        return "26";
      }
      if (keyRemode == "27" ) {
        ac.setFan(kPanasonicAcFanMed);
        ac.send();
        Serial.println("\nSpeed FAN: 3");
        delay(100);
        return "27";
      }
      if (keyRemode == "28" ) {
        ac.setFan(kPanasonicAcFanHigh);
        ac.send();
        Serial.println("\nSpeed FAN: 4");
        delay(100);
        return "28";
      }
      if (keyRemode == "29" ) {
        ac.setFan(kPanasonicAcFanMax);
        ac.send();
        Serial.println("\nSpeed FAN: 5");
        delay(100);
        return "29";
      }

      return "0";
}


// api nhiet do-do am
void temphum()
{
  int hum = dht.readHumidity(); 
  String h = String(hum);   //Đọc độ ẩm
  int temp = dht.readTemperature(); //Đọc nhiệt độ
  String t = String(temp); 
  Serial.print("Nhiet do: ");
  Serial.println(t);               //Xuất nhiệt độ
  Serial.print("Do am: ");
  Serial.println(h);               //Xuất độ ẩm
  Serial.println();  
  if (t.toInt() > 100 && h.toInt() > 100)
  {
    t = "100";
    h= "100";    
  }

  String url1 = httpUrl+"settemphum/1?temp="+t+"&hum="+h;
  String response1 = getApi(url1);
}
void firealarm()
{
  int value_fire = digitalRead(sensor_fire);
  String f = String(value_fire);
  Serial.print("Chay: ");
  Serial.println(value_fire);               
  Serial.println(); 
    String url3 = httpUrl+"setkeyfire/1?key="+f;
    String response2 = getApi(url3);
}
int oldTemp = 1;
void setTempAir() {
  String url4 = httpUrl+"gettempair/1";
  String tempAir = getApi(url4);
  int newTemp = tempAir.toInt();
  if(newTemp != oldTemp)
  {
    if (newTemp >= 16 && newTemp <= 30)
    {
      ac.setTemp(newTemp);
      ac.send();
      oldTemp = newTemp;      
    }
  }
}
