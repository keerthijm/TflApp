# TflApp
TFL Test App
TFL Test App:
1.	How to build the code
    a.	Download code from the link https://github.com/keerthijm/TflApp
    i.	Download in Zip format
        1.	Extract into local folder
        2.	Open TflApp.sln file – Either in Visual Studio 2019 or in Visual Studio Code
        3.	Note: require .Net 5.0 framework 
        4.	Set Command line arguments from Visual Studio-> Solution Explorer-> Right click on solution-> Properties -> Debug -> Application Arguments -> Enter the Value
        5.	In Visual Studio -> File->Debug -> Run
    ii.	Or Open with Visual Studio and repeat steps from the web link (iv, v)
2.	Changing App ID and API key
    a.	APP ID and API Key are stored under appsettings.json in the application
    b.	 ![image](https://user-images.githubusercontent.com/107592/129339146-f10a74ef-0bdb-4c24-b942-defc43c84365.png)

    c.	Change below configuration for the file appsettings.json under application TflApp and TestTflApp
    i.	  "app_id": "PLEASE ENTER YOUR APP_ID",
    ii.	  "app_key": "PLEASE ENTER YOUR APP_KEY",
3.	How to run the output – 2 options 
    a.	Download TflApp\Exe folder from the link above
        i.	You can find exe under the folder structure ...\TflApp\Exe\TflApp\ TflApp.exe 
        ii.	Change the appsettings.json file to add your app id and app key
        iii.	Run TflApp.exe in command prompt by providing argument input parameter
        iv.	Eg: C:\> .\RoadStatus.exe A2 or .\RoadStatus.exe A233
    b.	Run the application through the visual studio as per the instruction 1
4.	How to run any tests that you have written
    a.	Use TestTflApp to run tests 
    b.	Under Visual studio 2019 -> Tests Menu-> click on Run All Tests
5.	Any assumptions that you’ve made
    a.	I assume users of this has some basic knowledge of running code under visual studio 2019 or visual studio code
6.	Anything else you think is relevant
    a.	Below is the structure of the project
        i.	Main Program: main program where application starts working
        ii.	ApiService: Service which can GET information from apis, in future if require we can expand the service to support POST
        iii.	AppSettings – Where we store information related to APP ID and APP Key
        iv.	Common Constants – String constants used across the project are stored under the class
        v.	JsonTokenReaderUtility – used to parse Json token object into result string, in future we can extend it support other Json token formats
        vi.	Result into road object
            1.	Base Abstract class has Road class
            2.	Derived into Valid and Invalid Road class
            3.	Flow diagram as below:


![image](https://user-images.githubusercontent.com/107592/129338955-958f7644-847c-4c86-b4b4-c1e95a1fdcd7.png)



