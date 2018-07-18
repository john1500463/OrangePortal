import pypyodbc
import pandas
import threading
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText


def notification():
    msg = MIMEMultipart('alternative')
    msg['Subject'] = "Test"
    msg['From'] = "test@orange.com"
    msg['To'] = "haddiir.tarek@orange.com"
    text = "Group changed :D "
    part1 = MIMEText(text, 'plain')
    msg.attach(part1)
    s = smtplib.SMTP('mx-us.equant.com')
    s.sendmail('test@orange.com', 'john.sobhy@orange.com', msg.as_string())
    print ("Mail Sent")
    s.quit()

def ReadingSQL():
    connection = pypyodbc.connect('Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()
    SQLCommand = ("SELECT * FROM [Expedite].[dbo].['All_Incidents'];")
    cursor.execute(SQLCommand)
    results = cursor.fetchall()
    connection.close()
    return results

def ReadingFromExcel():
    df = pandas.read_excel('C:/Users/wkzw7370/PycharmProjects/Task2/NewExpedite.xls', sheet_name='Report 1')
    return df
# D:/Expedite/NewExpedite.xls
def InsertSQL(num):
    connection = pypyodbc.connect('Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()
    df=ReadingFromExcel()
    df['INC RES Resolution'][num] = str(df['INC RES Resolution'][num]).replace("'", " ")
    df['INC Summary'][num] = str(df['INC Summary'][num]).replace("'", " ")
    df['INC CI Site Group'][num] = str(df['INC CI Site Group'][num]).replace("'", " ")
    SQLCommand = ("INSERT INTO  [Expedite].[dbo].['All_Incidents'] ([INC Incident Number] ,[INC Priority],[INC Status],[INC Tier 2],[INC Tier 3],[AG Assignee],[INC CI Corporate ID],[INC CI Entity],[INC CI Site],[INC CI Site Group],[INC CI Region] ,[AG Assignee Manager Name],[AG Assigned Group Name],[AG M Email Address],[INC DS Submit Date],[INC DS Last Modified By Full Name],[INC DS Submitter Full Name],[INC RES Resolution],[AG Assignee Email Address],[INC CI Email Address],[RG Resolved By],[RG Resolved Group Name],[INC Summary]) VALUES ('"+str(df['INC Incident Number'][num])+"','"+str(df['INC Priority'][num])+"','"+str(df['INC Status'][num])+"','"+str(df['INC Tier 2'][num])+"','"+str(df['INC Tier 3'][num])+"','"+str(df['AG Assignee'][num])+"','"+str(df['INC CI Corporate ID'][num])+"','"+str(df['INC CI Entity'][num])+"','"+str(df['INC CI Site'][num])+"','"+str(df['INC CI Site Group'][num])+"','"+str(df['INC CI Region'][num])+"','"+str(df['AG Assignee Manager Name'][num])+"','"+str(df['AG Assigned Group Name'][num])+"','"+str(df['AG M Email Address'][num])+"',convert (datetime,'"+str(df['INC DS Submit Date'][num])+"'),'"+str(df['INC DS Last Modified By Full Name'][num])+"','"+str(df['INC DS Submitter Full Name'][num])+"',' "+str(df['INC RES Resolution'][num])+"','"+str(df['AG Assignee Email Address'][num])+"','"+str(df['INC CI Email Address'][num])+"','"+str(df['RG Resolved By'][num])+"','"+str(df['RG Resolved Group Name'][num])+"','"+str(df['INC Summary'][num])+"');")

    print (SQLCommand)

    cursor.execute(SQLCommand)
    connection.commit()
    if(str(df['INC DS Last Modified Date'][num])!= "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Modified Date] = convert (datetime,'"+str(df['INC DS Last Modified Date'][num])+"') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if(str(df['INC DS Closed Date'][num]) != "NaT"):
        SQLCommand = ( "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Closed Date] = convert (datetime,'" + str(df['INC DS Closed Date'][num]) + "') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if(str(df['INC DS Last Resolved Date'][num])!="NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Resolved Date] = convert (datetime,'" + str(df['INC DS Last Resolved Date'][num]) + "') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()


    connection.close()
    return




def EditSQL(num):
    connection = pypyodbc.connect('Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()

    df=ReadingFromExcel()
    df['INC RES Resolution'][num] = str(df['INC RES Resolution'][num]).replace("'", " ")
    df['INC Summary'][num] = str(df['INC Summary'][num]).replace("'", " ")
    df['INC CI Site Group'][num] = str(df['INC CI Site Group'][num]).replace("'", " ")
    SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC Status] = '"+str(df['INC Status'][num])+"' ,[INC Priority] = '"+str(df['INC Priority'][num])+"', [INC Tier 2] = '"+str(df['INC Tier 2'][num])+"',[INC Tier 3]= '"+str(df['INC Tier 3'][num])+"',[AG Assignee] = '"+str(df['AG Assignee'][num])+"',[INC CI Corporate ID]= '"+str(df['INC CI Corporate ID'][num])+"',[INC CI Entity]='"+str(df['INC CI Entity'][num])+"',[INC CI Site]= '"+str(df['INC CI Site'][num])+"',[INC CI Site Group]= '"+str(df['INC CI Site Group'][num])+"',[INC CI Region]='"+str(df['INC CI Region'][num])+"' ,[AG Assignee Manager Name]='"+str(df['AG Assignee Manager Name'][num])+"',[AG Assigned Group Name]='"+str(df['AG Assigned Group Name'][num])+"',[AG M Email Address]='"+str(df['AG M Email Address'][num])+"',[INC DS Last Modified By Full Name]='"+str(df['INC DS Last Modified By Full Name'][num])+"',[INC DS Submitter Full Name]= '"+str(df['INC DS Submitter Full Name'][num])+"',[INC RES Resolution]= '"+str(df['INC RES Resolution'][num])+"',[AG Assignee Email Address] ='"+str(df['AG Assignee Email Address'][num])+"',[INC CI Email Address]= '"+str(df['INC CI Email Address'][num])+"',[RG Resolved By]='"+str(df['RG Resolved By'][num])+"',[RG Resolved Group Name]= '"+str(df['RG Resolved Group Name'][num])+"',[INC Summary]='"+str(df['INC Summary'][num])+"' WHERE [INC Incident Number] = '" +str(df['INC Incident Number'][num]) +"';")
    print(SQLCommand)
    cursor.execute(SQLCommand)
    connection.commit()
    if (str(df['INC DS Last Modified Date'][num]) != "None" and str(df['INC DS Last Modified Date'][num]) != "nan" and str(df['INC DS Last Modified Date'][num]) != "NaT" ):
        SQLCommand = ( "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Modified Date] = convert (datetime,'" + str(df['INC DS Last Modified Date'][num]) + "') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if (str(df['INC DS Closed Date'][num]) != "None" and str(df['INC DS Closed Date'][num]) != "nan" and  str(df['INC DS Closed Date'][num]) != "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Closed Date] = convert (datetime,'" + str(df['INC DS Closed Date'][num]) + "') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if (str(df['INC DS Last Resolved Date'][num]) != "None" and  str(df['INC DS Last Resolved Date'][num]) != "nan" and  str(df['INC DS Last Resolved Date'][num]) != "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Resolved Date] = convert (datetime,'" + str(df['INC DS Last Resolved Date'][num]) + "') WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]) + "';")
    if (df['INC DS Last Resolved Date'][num] == "None" or str(df['INC DS Last Resolved Date'][num]) == "nan" or str(df['INC DS Last Resolved Date'][num]) == "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Resolved Date] = null WHERE [INC Incident Number] = '" + str( df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()

    connection.close()
    return


def mainFunction():

    threading.Timer(10.0, mainFunction).start()
    df= ReadingFromExcel()
    results=ReadingSQL()
    boolean = False
    a=0
    for x in range(len(df)):
        if (str(df['INC Priority'][x])=='nan'):
            df['INC Priority'][x]="None"
        if (str(df['INC Status'][x])=='nan'):
            df['INC Status'][x]="None"
        if (str(df['INC Tier 2'][x])=='nan'):
            df['INC Tier 2'][x]="None"
        if (str(df['INC Tier 3'][x])=='nan'):
            df['INC Tier 3'][x]="None"
        if (str(df['AG Assignee'][x])=='nan'):
            df['AG Assignee'][x]="None"
        if (str(df['INC CI Corporate ID'][x])=='nan'):
            df['INC CI Corporate ID'][x]="None"
        if (str(df['INC CI Entity'][x])=='nan'):
            df['INC CI Entity'][x]="None"
        if (str(df['INC CI Site'][x])=='nan'):
            df['INC CI Site'][x]="None"
        if (str(df['INC CI Site Group'][x])=='nan'):
            df['INC CI Site Group'][x]="None"
        if (str(df['INC CI Region'][x])=='NaT'):
            df['INC CI Region'][x]="None"
        if (str(df['AG Assignee Manager Name'][x])=='NaT'):
            df['AG Assignee Manager Name'][x]="None"
        if (str(df['AG Assigned Group Name'][x])=='NaT'):
            df['AG Assigned Group Name'][x]="None"
        if (str(df['AG M Email Address'][x])=='NaT'):
            df['AG M Email Address'][x]="None"
        if (str(df['INC DS Submit Date'][x])=='NaT'):
            df['INC DS Submit Date'][x]="None"
        if (str(df['INC DS Last Modified By Full Name'][x])=='NaT'):
            df['INC DS Last Modified By Full Name'][x]="None"
        if (str(df['INC DS Last Modified Date'][x])=='NaT'):
            df['INC DS Last Modified Date'][x]="None"
        if (str(df['INC DS Last Resolved Date'][x])=='NaT'):
            df['INC DS Last Resolved Date'][x]="None"
        if (str(df['INC DS Submitter Full Name'][x])=='NaT'):
            df['INC DS Submitter Full Name'][x]="None"
        if (str(df['INC RES Resolution'][x]) == 'NaT'):
            df['INC RES Resolution'][x] = "None"
        if (str(df['AG Assignee Email Address'][x]) == 'NaT'):
            df['AG Assignee Email Address'][x] = "None"
        if (str(df['INC CI Email Address'][x]) == 'NaT'):
            df['INC CI Email Address'][x] = "None"
        if (str(df['RG Resolved By'][x]) == 'NaT'):
            df['RG Resolved By'][x] = "None"
        if (str(df['RG Resolved Group Name'][x]) == 'NaT'):
            df['RG Resolved Group Name'][x] = "None"
        if (str(df['INC DS Closed Date'][x]) == 'NaT'):
            df['INC DS Closed Date'][x] = "None"
        if (str(df['INC Summary'][x]) == 'NaT'):
            df['INC Summary'][x] = "None"

    for excel in range(len(df)):
        boolean = False

        for SQL in range(len(results)):
           if (results[SQL][0]==df['INC Incident Number'][excel]):
              boolean = True
              df['INC RES Resolution'][excel] = str(df['INC RES Resolution'][excel]).replace("'", " ")
              df['INC Summary'][excel] = str(df['INC Summary'][excel]).replace("'", " ")
              df['INC CI Site Group'][excel] = str(df['INC CI Site Group'][excel]).replace("'", " ")

              boolean1 = False
              if (str(results[SQL][2]) != str(df['INC Status'][excel])):
                  print("1")
                  print(str(results[SQL][2]))
                  print(df['INC Status'][excel])
                  print (results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][1]) != str(df['INC Priority'][excel])):
                  print("2")
                  print((results[SQL][1]))
                  print(df['INC Priority'][excel])
                  print(results[SQL][0])
                  boolean1=True
              if(str(results[SQL][3]) != str(df['INC Tier 2'][excel])):
                  print("3")
                  print (results[SQL][3])
                  print(df['INC Tier 2'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][4]) != str(df['INC Tier 3'][excel])):
                  print("4")
                  print(results[SQL][4])
                  print(df['INC Tier 3'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (str(results[SQL][5]) != str(df['AG Assignee'][excel])):
                  print("5")
                  print(results[SQL][5])
                  print(df['AG Assignee'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (str(results[SQL][6]) != str(df['INC CI Corporate ID'][excel])):
                  print("6")
                  print  (results[SQL][6])
                  print(df['INC CI Corporate ID'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (str(results[SQL][7]) != str(df['INC CI Entity'][excel])):
                  print("7")
                  print (results[SQL][7])
                  print(df['INC CI Entity'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if( str(results[SQL][8]) != str(df['INC CI Site'][excel])):
                  print("8")
                  print(results[SQL][8])
                  print(df['INC CI Site'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][9]) != str(df['INC CI Site Group'][excel])):
                  print("9")
                  print(results[SQL][9])
                  print(df['INC CI Site Group'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][10]) != str(df['INC CI Region'][excel])):
                  print("10")
                  print(results[SQL][10])
                  print(df['INC CI Region'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][11]) != str(df['AG Assignee Manager Name'][excel])):
                  print("11")
                  print (results[SQL][11])
                  print(df['AG Assignee Manager Name'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][12]) != str(df['AG Assigned Group Name'][excel])):
                  print("12")
                  print (results[SQL][12])
                  print(df['AG Assigned Group Name'][excel])
                  print(results[SQL][0])
                  notification()
                  boolean1 = True
              if( str(results[SQL][13]) != str(df['AG M Email Address'][excel])):
                  print("13")
                  print (results[SQL][13])
                  print(df['AG M Email Address'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][14]) != str(df['INC DS Submit Date'][excel]) ):
                  print("14")
                  print(results[SQL][14])
                  print(df['INC DS Submit Date'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][15]) != str(df['INC DS Last Modified By Full Name'][excel])):
                  print("15")
                  print (results[SQL][15])
                  print(df['INC DS Last Modified By Full Name'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][16]) !=str(df['INC DS Last Modified Date'][excel]) ):
                  print("16")
                  print(results[SQL][16])
                  print(df['INC DS Last Modified Date'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][17]) != str(df['INC DS Last Resolved Date'][excel])):
                  print("17")
                  print(results[SQL][17])
                  print(df['INC DS Last Resolved Date'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][18]) != str(df['INC DS Submitter Full Name'][excel])):
                  print("18")
                  print (results[SQL][18])
                  print(df['INC DS Submitter Full Name'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (str(results[SQL][19]) != str(df['INC RES Resolution'][excel])):
                  print("19")
                  print (results[SQL][19])
                  print(df['INC RES Resolution'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (str(results[SQL][20]) != str(df['AG Assignee Email Address'][excel])):
                  print("20")
                  print (results[SQL][20])
                  print(df['AG Assignee Email Address'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][21]) != str(df['INC CI Email Address'][excel])):
                  print("21")
                  print(results[SQL][21])
                  print(df['INC CI Email Address'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if ( str(results[SQL][22]) != str(df['RG Resolved By'][excel])):
                  print("22")
                  print(results[SQL][22])
                  print(df['RG Resolved By'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if( str(results[SQL][23]) != str(df['RG Resolved Group Name'][excel])):
                  print("23")
                  print (results[SQL][23])
                  print(df['RG Resolved Group Name'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][24]) !=str(df['INC DS Closed Date'][excel])):
                  print("24")
                  print (results[SQL][24])
                  print(df['INC DS Closed Date'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if(str(results[SQL][25]) != str(df['INC Summary'][excel])):
                  print("25")
                  print(results[SQL][25])
                  print(df['INC Summary'][excel])
                  print(results[SQL][0])
                  boolean1 = True
              if (boolean1==True):
                  EditSQL(excel)



        if boolean == False:
            InsertSQL(excel)
            print(excel)


mainFunction()

