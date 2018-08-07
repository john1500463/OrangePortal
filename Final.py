import pypyodbc
import pandas
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
import sys


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
    print("Mail Sent")
    s.quit()


def DeleteClosedAndNotExpedited():
    connection = pypyodbc.connect(
        'Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()
    SQLCommand = (
        "DELETE FROM [Expedite].[dbo].['All_Incidents']WHERE [INC Incident Number] NOT IN (SELECT [Incident_ID] FROM [Expedite].[dbo].[Expedite_time]) And [Expedite].[dbo].['All_Incidents'].[INC Status]='Closed'")
    cursor.execute(SQLCommand)
    connection.commit()
    connection.close()
    print("Deleted")


def ReadingSQL():
    connection = pypyodbc.connect(
        'Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()
    SQLCommand = ("SELECT * FROM [Expedite].[dbo].['All_Incidents'];")
    cursor.execute(SQLCommand)
    results = cursor.fetchall()
    connection.close()
    return results


def ReadingFromExcel():
    df = pandas.read_excel('D:/Expedite/NewExpedite.xls', sheet_name='Report 1')
    return df


def InsertSQL(num, df):
    connection = pypyodbc.connect(
        'Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()

    df['INC RES Resolution'][num] = str(df['INC RES Resolution'][num]).encode('utf-8').replace("'", " ")
    df['INC Summary'][num] = str(df['INC Summary'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Site Group'][num] = str(df['INC CI Site Group'][num]).encode('utf-8').replace("'", " ")
    df['INC Tier 3'][num] = str(df['INC Tier 3'][num]).encode('utf-8').replace("'", " ")
    df['INC Tier 2'][num] = str(df['INC Tier 2'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Site'][num] = str(df['INC CI Site'][num]).encode('utf-8').replace("'", " ")
    df['AG Assignee'][num] = str(df['AG Assignee'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Corporate ID'][num] = str(df['INC CI Corporate ID'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Entity'][num] = str(df['INC CI Entity'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Region'][num] = str(df['INC CI Region'][num]).encode('utf-8').replace("'", " ")
    df['AG Assignee Manager Name'][num] = str(df['AG Assignee Manager Name'][num]).encode('utf-8').replace("'", " ")
    df['AG Assigned Group Name'][num] = str(df['AG Assigned Group Name'][num]).encode('utf-8').replace("'", " ")
    df['INC DS Last Modified By Full Name'][num] = str(df['INC DS Last Modified By Full Name'][num]).encode(
        'utf-8').replace("'", " ")
    df['INC DS Submitter Full Name'][num] = str(df['INC DS Submitter Full Name'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Entity'][num] = str(df['INC CI Entity'][num]).encode('utf-8').replace("'", " ")
    df['RG Resolved By'][num] = str(df['RG Resolved By'][num]).encode('utf-8').replace("'", " ")
    df['RG Resolved Group Name'][num] = str(df['RG Resolved Group Name'][num]).encode('utf-8').replace("'", " ")
    SQLCommand = (
                "INSERT INTO  [Expedite].[dbo].['All_Incidents'] ([INC Incident Number] ,[INC Priority],[INC Status],[INC Tier 2],[INC Tier 3],[AG Assignee],[INC CI Corporate ID],[INC CI Entity],[INC CI Site],[INC CI Site Group],[INC CI Region] ,[AG Assignee Manager Name],[AG Assigned Group Name],[AG M Email Address],[INC DS Submit Date],[INC DS Last Modified By Full Name],[INC DS Submitter Full Name],[INC RES Resolution],[AG Assignee Email Address],[INC CI Email Address],[RG Resolved By],[RG Resolved Group Name],[INC Summary]) VALUES ('" + str(
            df['INC Incident Number'][num]).encode('utf-8') + "','" + str(df['INC Priority'][num]).encode(
            'utf-8') + "','" + str(df['INC Status'][num]).encode('utf-8') + "','" + str(df['INC Tier 2'][num]).encode(
            'utf-8') + "','" + str(df['INC Tier 3'][num]).encode('utf-8') + "','" + str(df['AG Assignee'][num]).encode(
            'utf-8') + "','" + str(df['INC CI Corporate ID'][num]).encode('utf-8') + "','" + str(
            df['INC CI Entity'][num]).encode('utf-8') + "','" + str(df['INC CI Site'][num]).encode(
            'utf-8') + "','" + str(df['INC CI Site Group'][num]).encode('utf-8') + "','" + str(
            df['INC CI Region'][num]).encode('utf-8') + "','" + str(df['AG Assignee Manager Name'][num]).encode(
            'utf-8') + "','" + str(df['AG Assigned Group Name'][num]).encode('utf-8') + "','" + str(
            df['AG M Email Address'][num]).encode('utf-8') + "',convert (datetime,'" + str(
            df['INC DS Submit Date'][num]) + "'),'" + str(df['INC DS Last Modified By Full Name'][num]).encode(
            'utf-8') + "','" + str(df['INC DS Submitter Full Name'][num]).encode('utf-8') + "',' " + str(
            df['INC RES Resolution'][num]).encode('utf-8') + "','" + str(df['AG Assignee Email Address'][num]).encode(
            'utf-8') + "','" + str(df['INC CI Email Address'][num]).encode('utf-8') + "','" + str(
            df['RG Resolved By'][num]).encode('utf-8') + "','" + str(df['RG Resolved Group Name'][num]).encode(
            'utf-8') + "','" + str(df['INC Summary'][num]).encode('utf-8') + "');")
    print(SQLCommand)

    cursor.execute(SQLCommand)
    connection.commit()
    if (str(df['INC DS Last Modified Date'][num]) != "None" and str(
            df['INC DS Last Modified Date'][num]) != "nan" and str(df['INC DS Last Modified Date'][num]) != "NaT"):
        SQLCommand = (
                    "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Modified Date] = convert (datetime,'" + str(
                df['INC DS Last Modified Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
                df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()

    if (str(df['INC DS Last Resolved Date'][num]) != "None" and str(
            df['INC DS Last Resolved Date'][num]) != "nan" and str(df['INC DS Last Resolved Date'][num]) != "NaT"):
        SQLCommand = (
                    "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Resolved Date] =convert (datetime,'" + str(
                df['INC DS Last Resolved Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
                df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if (str(df['INC DS Closed Date'][num]) != "None" and str(df['INC DS Closed Date'][num]) != "nan" and str(
            df['INC DS Closed Date'][num]) != "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Closed Date] = convert (datetime,'" + str(
            df['INC DS Closed Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
            df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()

    connection.close()
    return


def EditSQL(num, df):
    connection = pypyodbc.connect(
        'Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()

    df['INC RES Resolution'][num] = df['INC RES Resolution'][num].encode('utf-8').replace("'", " ")
    df['INC Summary'][num] = df['INC Summary'][num].encode('utf-8').replace("'", " ")
    df['INC CI Site Group'][num] = df['INC CI Site Group'][num].encode('utf-8').replace("'", " ")
    df['INC Tier 3'][num] = str(df['INC Tier 3'][num]).encode('utf-8').replace("'", " ")
    df['INC Tier 2'][num] = str(df['INC Tier 2'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Site'][num] = str(df['INC CI Site'][num]).encode('utf-8').replace("'", " ")
    df['AG Assignee'][num] = str(df['AG Assignee'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Corporate ID'][num] = str(df['INC CI Corporate ID'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Entity'][num] = str(df['INC CI Entity'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Region'][num] = str(df['INC CI Region'][num]).encode('utf-8').replace("'", " ")
    df['AG Assignee Manager Name'][num] = str(df['AG Assignee Manager Name'][num]).encode('utf-8').replace("'", " ")
    df['AG Assigned Group Name'][num] = str(df['AG Assigned Group Name'][num]).encode('utf-8').replace("'", " ")
    df['INC DS Last Modified By Full Name'][num] = str(df['INC DS Last Modified By Full Name'][num]).encode(
        'utf-8').replace("'", " ")
    df['INC DS Submitter Full Name'][num] = str(df['INC DS Submitter Full Name'][num]).encode('utf-8').replace("'", " ")
    df['INC CI Entity'][num] = str(df['INC CI Entity'][num]).encode('utf-8').replace("'", " ")
    df['RG Resolved By'][num] = str(df['RG Resolved By'][num]).encode('utf-8').replace("'", " ")
    df['RG Resolved Group Name'][num] = str(df['RG Resolved Group Name'][num]).encode('utf-8').replace("'", " ")
    SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC Status] = '" + str(df['INC Status'][num]).encode(
        'utf-8') + "' ,[INC Priority] = '" + str(df['INC Priority'][num]).encode('utf-8') + "', [INC Tier 2] = '" + str(
        df['INC Tier 2'][num]).encode('utf-8') + "',[INC Tier 3]= '" + str(df['INC Tier 3'][num]).encode(
        'utf-8') + "',[AG Assignee] = '" + str(df['AG Assignee'][num]).encode(
        'utf-8') + "',[INC CI Corporate ID]= '" + str(df['INC CI Corporate ID'][num]).encode(
        'utf-8') + "',[INC CI Entity]='" + str(df['INC CI Entity'][num]).encode('utf-8') + "',[INC CI Site]= '" + str(
        df['INC CI Site'][num]).encode('utf-8') + "',[INC CI Site Group]= '" + str(df['INC CI Site Group'][num]).encode(
        'utf-8') + "',[INC CI Region]='" + str(df['INC CI Region'][num]).encode(
        'utf-8') + "' ,[AG Assignee Manager Name]='" + str(df['AG Assignee Manager Name'][num]).encode(
        'utf-8') + "',[AG Assigned Group Name]='" + str(df['AG Assigned Group Name'][num]).encode(
        'utf-8') + "',[AG M Email Address]='" + str(df['AG M Email Address'][num]).encode(
        'utf-8') + "',[INC DS Last Modified By Full Name]='" + str(df['INC DS Last Modified By Full Name'][num]).encode(
        'utf-8') + "',[INC DS Submitter Full Name]= '" + str(df['INC DS Submitter Full Name'][num]).encode(
        'utf-8') + "',[INC RES Resolution]= '" + str(df['INC RES Resolution'][num]).encode(
        'utf-8') + "',[AG Assignee Email Address] ='" + str(df['AG Assignee Email Address'][num]).encode(
        'utf-8') + "',[INC CI Email Address]= '" + str(df['INC CI Email Address'][num]).encode(
        'utf-8') + "',[RG Resolved By]='" + str(df['RG Resolved By'][num]).encode(
        'utf-8') + "',[RG Resolved Group Name]= '" + str(df['RG Resolved Group Name'][num]).encode(
        'utf-8') + "',[INC Summary]='" + str(df['INC Summary'][num]).encode(
        'utf-8') + "' WHERE [INC Incident Number] = '" + str(df['INC Incident Number'][num]).encode('utf-8') + "';")
    print(SQLCommand)
    cursor.execute(SQLCommand)
    connection.commit()
    if (str(df['INC DS Last Modified Date'][num]) != "None" and str(
            df['INC DS Last Modified Date'][num]) != "nan" and str(df['INC DS Last Modified Date'][num]) != "NaT"):
        SQLCommand = (
                    "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Modified Date] = convert (datetime,'" + str(
                df['INC DS Last Modified Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
                df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if (str(df['INC DS Closed Date'][num]) != "None" and str(df['INC DS Closed Date'][num]) != "nan" and str(
            df['INC DS Closed Date'][num]) != "NaT"):
        SQLCommand = ("UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Closed Date] = convert (datetime,'" + str(
            df['INC DS Closed Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
            df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()
    if (str(df['INC DS Last Resolved Date'][num]) != "None" and str(
            df['INC DS Last Resolved Date'][num]) != "nan" and str(df['INC DS Last Resolved Date'][num]) != "NaT"):
        SQLCommand = (
                    "UPDATE [Expedite].[dbo].['All_Incidents'] SET [INC DS Last Resolved Date] =convert (datetime,'" + str(
                df['INC DS Last Resolved Date'][num]) + "') WHERE [INC Incident Number] = '" + str(
                df['INC Incident Number'][num]) + "';")
        print(SQLCommand)
        cursor.execute(SQLCommand)
        connection.commit()

    connection.close()
    return


def UpdateTime():
    connection = pypyodbc.connect(
        'Driver={SQL Server};' 'Server=10.238.110.196;' 'Database=Expedite;' 'uid=sa; pwd=Orange@123$')
    cursor = connection.cursor()

    SQLCommand = ("UPDATE [Expedite].[dbo].[Last_Update_Time] Set [Last Updated Time]= GETDATE();")

    cursor.execute(SQLCommand)
    connection.commit()
    connection.close()
    print("Time Updated")
    return


def mainFunction():
    df = ReadingFromExcel()
    results = ReadingSQL()
    boolean = False
    a = 0

    for excel in range(len(df)):
        boolean = False

        for SQL in range(len(results)):
            if (str(results[SQL][0]).encode('utf-8') == str(df['INC Incident Number'][excel]).encode('utf-8')):
                boolean = True
                df['INC RES Resolution'][excel] = str(df['INC RES Resolution'][excel]).encode('utf-8').replace("'", " ")
                df['INC Summary'][excel] = str(df['INC Summary'][excel]).encode('utf-8').replace("'", " ")
                df['INC CI Site Group'][excel] = str(df['INC CI Site Group'][excel]).encode('utf-8').replace("'", " ")

                boolean1 = False
                if (str(results[SQL][2]).encode('utf-8') != str(df['INC Status'][excel]).encode('utf-8')):
                    print("1")
                    print(str(results[SQL][2]).encode('utf-8'))
                    print(str(df['INC Status'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                    elif (str(results[SQL][1]).encode('utf-8') != str(df['INC Priority'][excel]).encode('utf-8')):
                    print("2")
                    print(str(results[SQL][1]).encode('utf-8'))
                    print(str(df['INC Priority'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][3]).encode('utf-8') != str(df['INC Tier 2'][excel]).encode('utf-8')):
                    print("3")
                    print(str(results[SQL][3]).encode('utf-8'))
                    print(str(df['INC Tier 2'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][4]).encode('utf-8') != str(df['INC Tier 3'][excel]).encode('utf-8')):
                    print("4")
                    print(str(results[SQL][4]).encode('utf-8'))
                    print(str(df['INC Tier 3'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][5]).encode('utf-8') != str(df['AG Assignee'][excel]).encode('utf-8')):
                    print("5")
                    print(str(results[SQL][5]).encode('utf-8'))
                    print(str(df['AG Assignee'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][6]).encode('utf-8') != str(df['INC CI Corporate ID'][excel]).encode('utf-8')):
                    print("6")
                    print(str(results[SQL][6]).encode('utf-8'))
                    print(str(df['INC CI Corporate ID'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][7]).encode('utf-8') != str(df['INC CI Entity'][excel]).encode('utf-8')):
                    print("7")
                    print(str(results[SQL][7]).encode('utf-8'))
                    print(str(df['INC CI Entity'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if ( str(results[SQL][8]).encode('utf-8') != str(df['INC CI Site'][excel]).encode('utf-8')):
                    print("8")
                    print(str(results[SQL][8]).encode('utf-8'))
                    print(str(df['INC CI Site'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][9]).encode('utf-8') != str(df['INC CI Site Group'][excel]).encode('utf-8')):
                    print("9")
                    print(str(results[SQL][9]).encode('utf-8'))
                    print(str(df['INC CI Site Group'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][10]).encode('utf-8') != str(df['INC CI Region'][excel]).encode('utf-8')):
                    print("10")
                    print(str(results[SQL][10]).encode('utf-8'))
                    print(str(df['INC CI Region'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][11]).encode('utf-8') != str(df['AG Assignee Manager Name'][excel]).encode(
                        'utf-8')):
                    print("11")
                    print(str(results[SQL][11]).encode('utf-8'))
                    print(str(df['AG Assignee Manager Name'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][12]).encode('utf-8') != str(df['AG Assigned Group Name'][excel]).encode('utf-8')):
                    print("12")
                    print(str(results[SQL][12]).encode('utf-8'))
                    print(str(df['AG Assigned Group Name'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    # notification()
                    boolean1 = True
                else if (str(results[SQL][13]).encode('utf-8') != str(df['AG M Email Address'][excel]).encode('utf-8')):
                    print("13")
                    print(str(results[SQL][13]).encode('utf-8'))
                    print(str(df['AG M Email Address'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][14]) != str(df['INC DS Submit Date'][excel]) ):
                    print("14")
                    print(results[SQL][14])
                    print(df['INC DS Submit Date'][excel])
                    print(results[SQL][0])
                    boolean1 = True
                else if (str(results[SQL][15]).encode('utf-8') != str(
                        df['INC DS Last Modified By Full Name'][excel]).encode('utf-8')):
                    print("15")
                    print(str(results[SQL][15]).encode('utf-8'))
                    print(str(df['INC DS Last Modified By Full Name'][excel]).encode('utf-8'))
                    print(str(results[SQL][0].encode('utf-8')).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][16]) != str(df['INC DS Last Modified Date'][excel]) ):
                    print("16")
                    print(results[SQL][16])
                    print(df['INC DS Last Modified Date'][excel])
                    print(results[SQL][0].encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][17]) != str(df['INC DS Last Resolved Date'][excel])):
                    print("17")
                    print(results[SQL][17])
                    print(df['INC DS Last Resolved Date'][excel])
                    print(results[SQL][0])
                    boolean1 = True
                else if (str(results[SQL][18]).encode('utf-8') != str(df['INC DS Submitter Full Name'][excel]).encode('utf-8')):
                    print("18")
                    print(str(results[SQL][18].encode('utf-8')).encode('utf-8'))
                    print(str(df['INC DS Submitter Full Name'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][19]).encode('utf-8') != str(df['INC RES Resolution'][excel]).encode('utf-8')):
                    print("19")
                    print(str(results[SQL][19].encode('utf-8')).encode('utf-8'))
                    print(str(df['INC RES Resolution'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][20]).encode('utf-8') != str(df['AG Assignee Email Address'][excel]).encode('utf-8')):
                    print("20")
                    print(str(results[SQL][20]).encode('utf-8'))
                    print(str(df['AG Assignee Email Address'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][21]).encode('utf-8') != str(df['INC CI Email Address'][excel]).encode(
                        'utf-8')):
                    print("21")
                    print(str(results[SQL][21]).encode('utf-8'))
                    print(str(df['INC CI Email Address'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if ( str(results[SQL][22]).encode('utf-8') != str(df['RG Resolved By'][excel]).encode('utf-8')):
                    print("22")
                    print(str(results[SQL][22]).encode('utf-8'))
                    print(str(df['RG Resolved By'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][23]) != str(df['RG Resolved Group Name'][excel])):
                    print("23")
                    print(str(results[SQL][23]).encode('utf-8'))
                    print(str(df['RG Resolved Group Name'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                else if (str(results[SQL][24]) != str(df['INC DS Closed Date'][excel])):
                    print("24")
                    print(results[SQL][24])
                    print(df['INC DS Closed Date'][excel])
                    print(results[SQL][0])
                    boolean1 = True
                else if (str(results[SQL][25]).encode('utf-8') != str(df['INC Summary'][excel]).encode('utf-8')):
                    print("25")
                    print(str(results[SQL][25]).encode('utf-8'))
                    print(str(df['INC Summary'][excel]).encode('utf-8'))
                    print(str(results[SQL][0]).encode('utf-8'))
                    boolean1 = True
                if (boolean1 == True):
                    EditSQL(excel, df)

        if boolean == False:
            InsertSQL(excel, df)
            print(excel)

    DeleteClosedAndNotExpedited()
    UpdateTime()


reload(sys)
sys.setdefaultencoding('utf8')
mainFunction()



