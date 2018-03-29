/*Pines de entrada por cada boton*/
const int buttonPin1 = 2;   
const int buttonPin2 = 3;   
const int buttonPinLDR = 5;   
const int LDRSensor = 6;

const int buttonCount = 4;
int myPins[buttonCount]= {buttonPin1,buttonPin2,buttonPinLDR,LDRSensor}; /*Caution, actualmente el LDR enviara positivo a diferencia de los demas botones */

/* Salida de señal a nuestro relay e led indicando el status */
const int SignalPin1 = 13; 
const int SignalPin2 = 12; 


// Variables will change:
int buttonPressed;
int lastbuttonPressed;             // the current reading from the input pin

int Pin1State = LOW;
int Pin2State = LOW;

bool LDRActive = false;
int buttonState;             // the current reading from the input pin

int LDRSensorState = LOW;
int lastButtonState = LOW;   // the previous reading from the input pin
unsigned long lastDebounceTime = 0;  // the last time the output pin was toggled
unsigned long LDRStartTime = 0;  // the last time the output pin was toggled
unsigned long LDREndTime = 0;  // the last time the output pin was toggled

unsigned long debounceDelay = 50;    // the debounce time; increase if the output flickers
int LDRONDelay = 5 ; 
int LDROnTime = 5;

void setup() {
/*Recorremos con el array de los botones de entrada y los inicializamos*/
for(int i = 0; i < buttonCount;i++){
 pinMode(myPins[i], INPUT_PULLUP);
 }

//pinMode(LDRSensor, INPUT_PULLUP);
  pinMode(SignalPin1, OUTPUT);
  pinMode(SignalPin2, OUTPUT);
  Serial.begin(9600);
  InitialTest();
 
}
void loop() {
  
 int reading;
/*Recorremos todos los bonotes de entrada para ver cual se preciono*/

    for(int i = 0; i < buttonCount;i++){
        reading = digitalRead(myPins[i]);
         if (reading == LOW){
               buttonPressed = myPins[i];
               i = buttonCount;// esto es para hacer el exit
             }
        }  

if (LDRActive = true && lastButtonState == LOW) LDRActive = false;

        
     if (reading != lastButtonState || buttonPressed != lastbuttonPressed ) {
        lastDebounceTime = millis(); // Si hay una variacion se reinicia contador para debounce
      //  Serial.println(buttonPressed);
        }


     if ((millis() - lastDebounceTime) > debounceDelay) {   // Si ya paso el tiempo de debounce, se toma valor actual del boton presionado

        
        
         if (LDRActive && buttonPressed == LDRSensor) {
        
                 if (reading != LDRSensorState){
                     LDRSensorState = reading; // Cambio el estado del sensor LDR
                     if (LDRSensorState == LOW){ // Se activo el ldr por la noche
                          Serial.println("NIGTH") ;
                          LDRStartTime = millis() + (LDRONDelay  * 1000) ;
                          LDREndTime = LDRStartTime + (LDRONDelay  * 1000) ;
                     }
                   else{
                       Serial.println("DAY") ;
                       LDRStartTime = 0;
                      }
                   
                 }
Serial.println((String)LDRStartTime + "/" + (String) millis() ) ;
                 
                 if(LDRStartTime>0 && ( millis()> LDRStartTime  &&  millis() < LDREndTime   ) ){
                //  Serial.println("On");
                    Pin1State = HIGH;
                 }
                 else{
                    // Serial.println("Off");
                     Pin1State = LOW;
                 }
            
             }


              else
        {
           if (reading != buttonState) { // ha cambiado el estado del boton a el último estado esto sera para todo excepto el sensor ldr
                 buttonState = reading;
                 
                 if(buttonState == LOW){
                  if (buttonPressed == buttonPin1  ) { // Button 1 Pushed
                     Pin1State = !Pin1State;
                     LDRActive =false;
                  }
                  else 
                  if (buttonPressed == buttonPin2  )  { // Button 2 Pushed
                     Pin2State = !Pin2State;
                     LDRActive =false;
                   }
                  else 
                  if (buttonPressed == buttonPinLDR  )  { // LDR Button pushed -> LDR flag activation
                    LDRActive = !LDRActive;
                    Pin1State = LOW;
                    Pin2State = LOW;
                    if (LDRActive)
                    Serial.println("LDR Activated") ;
                    else
                    Serial.println("LDR Deactivated") ;
                  }
                  else
                  if (LDRActive && buttonPressed == LDRSensor )  { // Señal del sensor LDR
                  }
                }
               
            }   
         

        }






             
             
             
     }

digitalWrite(SignalPin1, Pin1State);
digitalWrite(SignalPin2, Pin2State);

lastButtonState = reading;  
lastbuttonPressed = buttonPressed;


}


void InitialTest(){
  digitalWrite(SignalPin1, HIGH);
  delay(500);
  digitalWrite(SignalPin1, LOW);
  delay(250);
  digitalWrite(SignalPin2, HIGH);
  delay(500);
  digitalWrite(SignalPin2, LOW);
  delay(250);
  digitalWrite(SignalPin1, HIGH);
  digitalWrite(SignalPin2, HIGH);
  delay(500);
  digitalWrite(SignalPin1, LOW);
  digitalWrite(SignalPin2, LOW);
  }
