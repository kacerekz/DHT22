# Measuring temperature and humidity with a DHT22 + data measured over 2 weeks.

I used an Arduino clone and <a href="https://navody.dratek.cz/navody-k-produktum/teplotni-senzor-dht11.html">this</a> tutorial to write data from the sensor to a serial port. The data is written in lines in the following format (you should be able to easily modify the Arduino tutorial code to make the output look like this):

![image](https://user-images.githubusercontent.com/41385344/113910662-613cbd00-97d9-11eb-8b97-38382a3b1d1e.png)

where columns are separated by a semicolon.


The output of [ReadSerial](ReadSerial) looks like this:

![image](https://user-images.githubusercontent.com/41385344/113907518-c098ce00-97d5-11eb-9e17-961e3a98be70.png)

The data is continuously written to one file (../../tmp_hum.csv), and backed up to another (../../tmp_hum_backup.csv) every 15 minutes. You can run it as, for example:

```
ReadSerial COM4
```

Your serial port may vary :)

Once done with your experiments, you can use [SensorDataParser](SensorDataParser) to make the output a little prettier and separate each day into its own file. They get a little big, as the sensor captures approximately every 2s. Run it as:

```
SensorDataParser tmp_hum_backup.csv
```

You can see the output in the [data](data) folder. "Index" is the time in seconds. I've also made a short presentation with graphs that you can see [here](https://github.com/kacerekz/DHT22/blob/main/M%C4%9B%C5%99en%C3%AD%20teploty%20a%20vlhkosti.pdf).

I'm publishing this project because I had a little trouble finding any examples of writing data from an Arduino to a file on the PC it's connected to. I don't know if this is the best way, but it worked for me. Also, a heads up: sometimes the sensor data arrives a little scrambled and ReadSerial will crash. Wrap the parsing in a try-catch if that's an issue for you (I did it in SensorDataParser if you need an example). Also, tmp_hum.csv will overwrite every time you start the program. The backup file gets appended to, so you never lose more than 15 minutes of data (but you could easily modify that too, or contact me if you need help).

This should also work for the DHT11 with no changes.

You can find a super interesting experiment measuring the accuracy of DHT22s [here](https://www.kandrsmith.org/RJS/Misc/Hygrometers/calib_dht22.html).
