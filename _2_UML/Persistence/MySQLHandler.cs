using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2_UML.Models;
using _2_UML.Exceptions;
using System.Collections.ObjectModel;

namespace _2_UML.Persistence
{
    public static class MySQLHandler
    {
        static void MySQLHandlerStart()
        {
            Connectionstring = defaultConnection;
        }

        /// <summary>
        /// Local Connection
        /// </summary>
        private static readonly string defaultConnection = "Server=127.0.0.1;Database=erm_bewerbungen;Uid=root;Pwd=;";

        //Main connectionstring
        private static string Connectionstring { get; set; }

        private static MySqlConnection connection;

        //This is the result of sql-SELECT-queries. Must be cleared before a new query is executed!
        private static DataSet Result { get; set; }

        public static string Errormessage { get; private set; }

        /// <summary>
        /// Checks whether a connection to the database has already been established and, if neccessary, tries to create one.
        /// </summary>
        /// <returns>Is the database now connected?</returns>
        private static bool IsConnected()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                Connectionstring = defaultConnection;

                try
                {
                    connection = new MySqlConnection(Connectionstring);
                    connection.Open();
                }
                catch (MySqlException ex)
                {
                    string errortext = "Es konnte keine Verbindung zur Datenbank hergestellt werden. Fehlermeldung: '" + ex + "'";
                    Errormessage = errortext;
                    return false;
                }
                catch (Exception e)
                {
                    string errortext = "Es konnte keine Verbindung zur Datenbank hergestellt werden. Fehlermeldung: '" + e + "'";
                    Errormessage = errortext;
                    return false;
                }              
            }
            return true;
        }

        /// <summary>
        /// This is the method that is usually called for executing multiple statements within one transaction. 
        /// Executes the given sql commands and if we hava a result from a query, it gets saved in the DataSet "result".
        /// In general not suited for multiple select queries! Only the results of the last query will be saved in "result", so only experienced users
        /// should try to use multiple selects.
        /// Errors can be read by accessing the string "Errormessage".
        /// </summary>
        /// <param name="commands">A list of custom SqlCommands that shall be executed</param>
        /// <returns>If all commands were successful. If even one didn't work, no commands were executed</returns>
        private static bool ExecuteSQL(List<SqlCommand> commands)
        {
            bool status = true;
            Errormessage = String.Empty;
            Result = new DataSet();

            if (IsConnected())
            {
                //Begin transaction
                using (MySqlTransaction trans = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (SqlCommand command in commands)
                        {
                            MySqlCommand newCommand = new MySqlCommand(command.Commandtext, connection, trans);

                            //Insert parameters for prepared statements
                            if (command.Parameters != null)
                            {
                                foreach (KeyValuePair<string, string> parameter in command.Parameters)
                                {
                                    newCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                                }
                            }

                            newCommand.ExecuteNonQuery();

                            if (IsSelectQuery(command.Commandtext))
                            {
                                if (SaveResults(newCommand) == false)
                                    throw new CustomMysqlException("Es konnten keine Daten aus der Datenbank ausgelesen werden");
                            }
                        }

                        //Once every command has successfully been executed, the transaction will be commited.
                        trans.Commit();
                    }

                    catch (CustomMysqlException cex)
                    {
                        Errormessage = cex.Message;
                        status = false;
                    }
                    catch (MySqlException ex)
                    {
                        Errormessage = ex.Message;
                        status = false;
                    }
                    catch (Exception e)
                    {
                        Errormessage = e.Message;
                        status = false;
                    }
                    finally
                    {
                        Close();
                    }
                }
            }
            else
            {
                Errormessage = "Es konnte keine Verbindung zur Datenbank hergestellt werden.";
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Saves the results of a successful sql-query in the "Result" DataSet
        /// </summary>
        /// <param name="newCommand"></param>
        /// <returns></returns>
        private static bool SaveResults(MySqlCommand newCommand)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(newCommand);
            dataAdapter.Fill(Result);

            if (CheckResults() == false)
            {
                Errormessage = "Es konnten keine Daten aus der Datenbank ausgelesen werden";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// This is the method that is usually called. Executes the sql command and if we hava a result from a query, it gets saved in the DataSet "result"
        /// Errors can be read by accessing the string "Errormessage".
        /// </summary>
        /// <param name="sqlString">The string that is going to be executed</param>
        /// <param name="parameters">The parameters of the critical variables, used to prevent SQL-injection</param>
        private static bool ExecuteSQL(string sqlString,List<KeyValuePair<string,string>> parameters=null)
        {
            bool everythingOkay = true;
            Errormessage = String.Empty;
            Result = new DataSet();

            if (IsConnected())
            {
                MySqlCommand newCommand = new MySqlCommand();

                //Do we have parameters for prepared statements?
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, string> parameter in parameters)
                    {
                        newCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }
                newCommand.Connection = connection;
                newCommand.CommandText = sqlString;
                
                bool isSelect = IsSelectQuery(sqlString);

                try
                {
                    newCommand.Prepare();
                    newCommand.ExecuteNonQuery();

                    if (isSelect)
                    {
                        SaveResults(newCommand);
                    }
                }
                catch (MySqlException ex)
                {
                    string errortext = "Ungültiger SQL-Code: '" + sqlString + "'. Fehlermeldung: '" + ex + "'";
                    Errormessage=errortext;
                    everythingOkay = false;
                }
                catch (Exception e)
                {
                    string errortext = "Es ist bei der Ausführung des Befehls '" + sqlString + "' ein Fehler aufgetreten. Fehlermeldung: '" + e + "'";
                    Errormessage=errortext;
                    everythingOkay = false;
                }
                finally
                {
                    Close();
                }                
            }
            else
            {
                Errormessage="Es konnte keine Verbindung zur Datenbank hergestellt werden.";
                everythingOkay = false;
            }
            return everythingOkay;
        }

        /// <summary>
        /// Speichert Änderungen, die an einem Ausbilder vorgenommen wurden
        /// </summary>
        /// <param name="ausbilder"></param>
        /// <returns>Änderungen erfolgreich?</returns>
        public static bool UpdateAusbilder(Ausbilder ausbilder)
        {
            string sql = "UPDATE ausbilder";
            sql += " SET vorname=@0, name=@1, telefon=@2, e_mail=@3";
            sql += " WHERE id=@5";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",ausbilder.Vorname),
                new KeyValuePair<string, string>("@1",ausbilder.Name),
                new KeyValuePair<string, string>("@2",ausbilder.Telefonnummer),
                new KeyValuePair<string, string>("@3",ausbilder.EMail),
                new KeyValuePair<string, string>("@5",ausbilder.Id.ToString()),
            };

            
            if (ExecuteSQL(sql, parameters))
            {
                return true;
            }
            return false;
            

        }

        /// <summary>
        /// Speichert Änderungen, die an einem Teilnehmer vorgenommen wurden
        /// </summary>
        /// <param name="ausbilder"></param>
        /// <returns>Änderungen erfolgreich?</returns>
        public static bool UpdateTeilnehmer(Teilnehmer teilnehmer)
        {           
            List<SqlCommand> updateCommands = new List<SqlCommand>();

            updateCommands.Add(UpdateTeilnehmerCommand(teilnehmer));
            updateCommands.Add(UpdateAdressCommand(teilnehmer.Adresse));

            return ExecuteSQL(updateCommands);
        }

        /// <summary>
        /// Forms an sql command which updates the properties of a teilnehmer 
        /// </summary>
        /// <param name="teilnehmer">The new teilnehmer that shall be saved</param>
        /// <returns>The finished command</returns>
        private static SqlCommand UpdateTeilnehmerCommand(Teilnehmer teilnehmer)
        {
            string sql = "UPDATE teilnehmer";
            sql += " SET vorname=@0, name=@1, telefon=@2, e_mail=@3, fk_beruf=@4, fk_ausbilder=@5";
            sql += " WHERE id=@8";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",teilnehmer.Vorname),
                new KeyValuePair<string, string>("@1",teilnehmer.Name),
                new KeyValuePair<string, string>("@2",teilnehmer.Telefonnummer),
                new KeyValuePair<string, string>("@3",teilnehmer.EMail),
                new KeyValuePair<string, string>("@4",teilnehmer.Beruf.Id.ToString()),
                new KeyValuePair<string, string>("@5",teilnehmer.Ausbilder.Id.ToString()),
                new KeyValuePair<string, string>("@8",teilnehmer.Id.ToString()),
            };

            return new SqlCommand
            {
                Commandtext = sql,
                Parameters = parameters,
            };
        }

        /// <summary>
        /// Forms an sql command which updates the properties of an adress
        /// </summary>
        /// <param name="adresse">The new adress that shall be saved</param>
        /// <returns>The finished command</returns>
        private static SqlCommand UpdateAdressCommand (Adresse adresse)
        {
            string sql = "UPDATE adresse SET ort=@0, postleitzahl=@1, straße=@2, hausnummer=@3, land=@4";
            sql += " WHERE id=@5";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",adresse.Ort),
                new KeyValuePair<string, string>("@1",adresse.Postleitzahl),
                new KeyValuePair<string, string>("@2",adresse.Straße),
                new KeyValuePair<string, string>("@3",adresse.Hausnummer),
                new KeyValuePair<string, string>("@4",adresse.Land),
                new KeyValuePair<string, string>("@5",adresse.Id.ToString()),
            };

            return new SqlCommand
            {
                Commandtext = sql,
                Parameters = parameters
            };
        }

 

        /// <summary>
        /// Fügt einen neuen Ausbilder zur Liste hinzu
        /// </summary>
        /// <param name="ausbilder"></param>
        /// <returns>Einfügen erfolgreich</returns>
        public static bool AddNewAusbilder(Ausbilder ausbilder)
        {
            //Since the individual queries need the results of the previous one, we can't use a list of sqlcommands
            if (AddNewNutzer(ausbilder.Nutzer))
            {
                if (NeuestenNutzerAuswaehlen())
                {
                    ausbilder.Nutzer.Id = (int)Result.Tables[0].Rows[0][0];

                    string sql = "INSERT INTO ausbilder (vorname, name, telefon, e_mail, fk_nutzer)";
                    sql += " VALUES(@0,@1,@2,@3,@4);";

                    List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("@0",ausbilder.Vorname),
                        new KeyValuePair<string, string>("@1",ausbilder.Name),
                        new KeyValuePair<string, string>("@2",ausbilder.Telefonnummer),
                        new KeyValuePair<string, string>("@3",ausbilder.EMail),
                        new KeyValuePair<string, string>("@4",ausbilder.Nutzer.Id.ToString()),
                    };

                    if (ExecuteSQL(sql, parameters))
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        /// <summary>
        /// Fügt einen neuen Teilnehmer zur Liste hinzu
        /// </summary>
        /// <param name="ausbilder"></param>
        /// <returns>Einfügen erfolgreich</returns>
        public static bool AddNewTeilnehmer(Teilnehmer teilnehmer)
        {
            if (AddNewNutzer(teilnehmer.Nutzer))
            {
                if (NeuestenNutzerAuswaehlen())
                {
                    teilnehmer.Nutzer.Id = (int)Result.Tables[0].Rows[0][0];
                    if (AddNewAdresse(teilnehmer.Adresse))
                    {
                        if (NeuesteAdresseAuswaehlen())
                        {
                            teilnehmer.Adresse.Id = (int)Result.Tables[0].Rows[0][0];

                            string sql = "INSERT INTO teilnehmer (vorname, name, telefon, e_mail, fk_beruf, fk_ausbilder, fk_adresse, fk_nutzer)";
                            sql += " VALUES(@0,@1,@2,@3,@4,@5,@6,@7);";

                            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("@0",teilnehmer.Vorname),
                                new KeyValuePair<string, string>("@1",teilnehmer.Name),
                                new KeyValuePair<string, string>("@2",teilnehmer.Telefonnummer),
                                new KeyValuePair<string, string>("@3",teilnehmer.EMail),
                                new KeyValuePair<string, string>("@4",teilnehmer.Beruf.Id.ToString()),
                                new KeyValuePair<string, string>("@5",teilnehmer.Ausbilder.Id.ToString()),
                                new KeyValuePair<string, string>("@6",teilnehmer.Adresse.Id.ToString()),
                                new KeyValuePair<string, string>("@7",teilnehmer.Nutzer.Id.ToString()),
                            };

                            if (ExecuteSQL(sql, parameters))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Fügt eine neue Adresse zur Liste hinzu
        /// </summary>
        /// <param name="ausbilder"></param>
        /// <returns>Einfügen erfolgreich</returns>
        private static bool AddNewAdresse(Adresse adresse)
        {
            string sql = "INSERT INTO adresse(ort, postleitzahl, straße, hausnummer, land)";
            sql += " VALUES @0,@1,@2,@3,@4;";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",adresse.Ort),
                new KeyValuePair<string, string>("@1",adresse.Postleitzahl),
                new KeyValuePair<string, string>("@2",adresse.Straße),
                new KeyValuePair<string, string>("@3",adresse.Hausnummer),
                new KeyValuePair<string, string>("@4",adresse.Land),
            };

            if (ExecuteSQL(sql, parameters))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sucht die id der zuletzt eingefügten Adresse heraus
        /// </summary>
        /// <returns></returns>
        private static bool NeuesteAdresseAuswaehlen()
        {
            string sql = "SELECT MAX(a.id) FROM adresse as a;";
            if (ExecuteSQL(sql))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sucht die Id des zuletzt hinzugefügten Nutzers heraus
        /// </summary>
        /// <returns></returns>
        private static bool NeuestenNutzerAuswaehlen()
        {
            string sql = "SELECT MAX(n.id) FROM nutzer as n;";
            if (ExecuteSQL(sql))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fügt neuen Nutzer zur Tabelle "nutzer" hinzu
        /// </summary>
        /// <param name="nutzer"></param>
        /// <returns></returns>
        private static bool AddNewNutzer(Nutzer nutzer)
        {
            string sql0 = "INSERT INTO nutzer (passwort, fk_sicherheitsabfrage, fk_nutzertyp, sicherheitsantwort)";
            sql0 += " VALUES(@0,@1,@2,@3);";

            List<KeyValuePair<string, string>> parameters0 = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("@0",nutzer.Passwort),
                    new KeyValuePair<string, string>("@1",nutzer.Sicherheitsfrage.Id.ToString()),
                    new KeyValuePair<string, string>("@2",nutzer.Nutzertyp.Id.ToString()),
                    new KeyValuePair<string, string>("@3",nutzer.Sicherheitsantwort),
                };

            if (ExecuteSQL(sql0, parameters0))
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Überprüft die eigegebenen Logindaten
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckLogin(string email, string password)
        {
            if (CheckLoginTeilnehmer(email, password) == false)
            {
                return false;
            }

            if (Result.Tables[0].Rows.Count != 1)
            {
                CheckLoginAusbilder(email, password);

                if (Result.Tables[0].Rows.Count != 1)
                {
                    return false;
                }
            }

            SaveUser();
            return true;
        }

        /// <summary>
        /// Ensures that a working DataSet was successfully created
        /// </summary>
        /// <returns></returns>
        private static bool CheckResults()
        {
            if (Result != null)
            {
                if (Result.Tables.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Speichert die Eigenschaften des aktuellen Nutzers unter Application.Current.Properties ab
        /// </summary>
        private static void SaveUser()
        {
            Int32.TryParse(Result.Tables[0].Rows[0][5].ToString(), out int IdNeu);
            Int32.TryParse(Result.Tables[0].Rows[0][6].ToString(), out int IdNutzerNeu);

            if (Result.Tables[0].Rows[0][4].ToString() == "Teilnehmer")
            {
                Int32.TryParse(Result.Tables[0].Rows[0][15].ToString(), out int IdAusbilderNeu);

                Teilnehmer teilnehmer = new Teilnehmer
                {
                    Vorname = $"{Result.Tables[0].Rows[0][0]}",
                    Name = $"{Result.Tables[0].Rows[0][1]}",
                    Telefonnummer = $"{Result.Tables[0].Rows[0][2]}",
                    EMail = $"{Result.Tables[0].Rows[0][3]}",
                    Nutzer = new Nutzer
                    {
                        Id = IdNutzerNeu,
                        Nutzertyp = new Nutzertyp
                        {
                            Typ = $"{Result.Tables[0].Rows[0][4]}"
                        },
                    },
                    Id = IdNeu,
                    Beruf = new Beruf
                    {
                        Bezeichnung = $"{Result.Tables[0].Rows[0][7]}",
                    },
                    Adresse = new Adresse
                    {
                        Ort = $"{Result.Tables[0].Rows[0][8]}",
                        Postleitzahl = $"{Result.Tables[0].Rows[0][9]}",
                        Straße = $"{Result.Tables[0].Rows[0][10]}",
                        Hausnummer = $"{Result.Tables[0].Rows[0][11]}",
                        Land = $"{Result.Tables[0].Rows[0][12]}",
                    },
                    Ausbilder = new Ausbilder
                    {
                        Vorname = $"{Result.Tables[0].Rows[0][13]}",
                        Name = $"{Result.Tables[0].Rows[0][14]}",
                        Id = IdAusbilderNeu,
                    },
                };
                Nutzereinstellungen.EinstellungenSpeichern(teilnehmer);
            }
            else
            {
                Ausbilder ausbilder = new Ausbilder
                {
                    Vorname = $"{Result.Tables[0].Rows[0][0]}",
                    Name = $"{Result.Tables[0].Rows[0][1]}",
                    Telefonnummer = $"{Result.Tables[0].Rows[0][2]}",
                    EMail = $"{Result.Tables[0].Rows[0][3]}",
                    Nutzer = new Nutzer
                    {
                        Id = IdNutzerNeu,
                        Nutzertyp = new Nutzertyp
                        {                            
                            Typ = $"{Result.Tables[0].Rows[0][4]}"
                        },
                    },
                    Id = IdNeu,
                };
                Nutzereinstellungen.EinstellungenSpeichern(ausbilder);
            }
        }

        /// <summary>
        /// Durchsucht nch einem Teilnehmer mit den eingegebenen Anmeldedaten
        /// </summary>
        /// <param name="email">Eingebene E-Mail-Adresse</param>
        /// <param name="password">Eingegebenes Passwort</param>
        private static bool CheckLoginTeilnehmer(string email, string password)
        {
            string sql = "SELECT t.vorname, t.name, t.telefon, t.e_mail, nt.nutzertyp, t.id, n.id,";
            sql += " b.bezeichnung, a.ort, a.postleitzahl, a.straße, a.hausnummer, a.land, au.vorname, au.name, au.id";
            sql += " FROM teilnehmer as t";
            sql += " LEFT JOIN nutzer as n ON t.fk_nutzer=n.id";
            sql += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp=nt.id";
            sql += " LEFT JOIN beruf as b ON t.fk_beruf=b.id";
            sql += " LEFT JOIN adresse as a ON t.fk_adresse=a.id";
            sql += " LEFT JOIN ausbilder as au ON t.fk_ausbilder=au.id";
            sql += " WHERE t.e_mail = @0 AND n.passwort = @1;";


            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",email),
                new KeyValuePair<string, string>("@1",password),
            };

            if (ExecuteSQL(sql, parameters))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Durchsucht nch einem Ausbilder mit den eingegebenen Anmeldedaten
        /// </summary>
        /// <param name="email">Eingebene E-Mail-Adresse</param>
        /// <param name="password">Eingegebenes Passwort</param>
        private static bool CheckLoginAusbilder(string email, string password)
        {
            string sql = "SELECT au.vorname, au.name, au.telefon, au.e_mail, nt.nutzertyp, au.id, n.id";

            sql += " FROM ausbilder as au";
            sql += " LEFT JOIN nutzer as n ON au.fk_nutzer=n.id";
            sql += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp=nt.id";
            sql += " WHERE au.e_mail = @0 AND n.passwort = @1;";


            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",email),
                new KeyValuePair<string, string>("@1",password),
            };

            if (ExecuteSQL(sql, parameters))
            {
                return true;
            }
            return false;
        }

        /*
        public static List<Firma> AlleFirmen()
        {
            string sql = "SELECT f.id, f.name, f.bewerbung_telefon, f.bewerbung_e_mail, f.beschreibung";
            sql += ", a.ort, a.postleitzahl, a.straße, a.hausnummer, a.land";
            sql += " FROM firma as f";
            sql += " LEFT JOIN adresse as a ON f.fk_adresse=a.id";
            sql += " LEFT JOIN abteilung as ab ON ab.fk_firma=f.id";
            sql += " LEFT JOIN bewerbung as b ON b.fk_abteilung=ab.id";
        }
        */


        private static SqlCommand SelectFromAusbilder()
        {
            string sql = "SELECT au.id, au.vorname, au.name, au.telefon, au.e_mail";
            sql += ", n.id, nt.nutzertyp";
            sql += " FROM ausbilder as au";
            sql += " LEFT JOIN nutzer as n ON au.fk_nutzer=n.id";
            sql += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp=nt.id";

            return new SqlCommand
            {
                Commandtext = sql,
            };
        } 


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Ausbilder> SelectAusbilder()
        {
            List<Ausbilder> AllAusbilder = new List<Ausbilder>();

            string sql = "SELECT au.id, au.vorname, au.name, au.telefon, au.e_mail";
            sql += ", n.id, nt.nutzertyp";
            sql += " FROM ausbilder as au";
            sql += " LEFT JOIN nutzer as n ON au.fk_nutzer=n.id";
            sql += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp=nt.id";
            if(ExecuteSQL(sql))
            {
                for (int i=0; i < Result.Tables[0].Rows.Count;i++)
                {
                    AllAusbilder.Add
                    (
                        new Ausbilder
                        {
                            Id = (int)Result.Tables[0].Rows[i][0],
                            Vorname = Result.Tables[0].Rows[i][1].ToString(),
                            Name = Result.Tables[0].Rows[i][2].ToString(),
                            Telefonnummer = Result.Tables[0].Rows[i][3].ToString(),
                            EMail = Result.Tables[0].Rows[i][4].ToString(),
                            Nutzer = new Nutzer
                            {
                                Id = (int)Result.Tables[0].Rows[i][5],
                                Nutzertyp= new Nutzertyp
                                {
                                    Typ = Result.Tables[0].Rows[i][6].ToString(),
                                }
                            }
                        }
                    );
                }
            }

            return AllAusbilder;            
        }

        public static bool DeleteAusbilder(Ausbilder ausbilder)
        {
            List<SqlCommand> deleteCommands = new List<SqlCommand>();

            List<int> ausbilderIds = new List<int>();
            List<int> ausbilderNutzerIds = new List<int>();
            ausbilderIds.Add(ausbilder.Id);
            ausbilderNutzerIds.Add(ausbilder.Nutzer.Id);

            deleteCommands.Add(DeleteFromAusbilder(ausbilderIds));
            deleteCommands.Add(DeleteFromNutzer(ausbilderNutzerIds));
            deleteCommands.Add(UpdateTeilnehmer(ausbilder.Id));


            return ExecuteSQL(deleteCommands);
        }

        /// <summary>
        /// Method that creates the command to delete from the table 'ausbilder' where the conditons are met
        /// </summary>
        /// <param name="conditions">The id's of the ausbilder we want to delete</param>
        /// <returns>The finished command</returns>
        private static SqlCommand DeleteFromAusbilder(List<int> ids)
        {
            List<KeyValuePair<string, string>> conditions = new List<KeyValuePair<string, string>>(); 

            string sql = "DELETE FROM ausbilder WHERE";

            for (int i = 0; i < ids.Count; i++)
            {
                sql += $" id=@{i}";
                if (i + 1 < ids.Count)
                {
                    sql += " OR";
                }

                conditions.Add(new KeyValuePair<string, string>($"@{i}", ids[i].ToString()));

            }
            sql += ";";

            return new SqlCommand
            {
                Commandtext = sql,
                Parameters = conditions
            };
        }

        /// <summary>
        /// Method that creates the command to delete from the table 'nutzer' where the conditons are met
        /// </summary>
        /// <param name="ids">The id's of the nutzer we want to delete</param>
        /// <returns>The finished command</returns>
        private static SqlCommand DeleteFromNutzer(List<int> ids)
        {
            List<KeyValuePair<string, string>> conditions = new List<KeyValuePair<string, string>>();
            string sql = "DELETE FROM nutzer WHERE";

            for (int i = 0; i < ids.Count; i++)
            {
                sql += $" id=@{i}";
                if (i + 1 < ids.Count)
                {
                    sql += " OR";
                }

                conditions.Add(new KeyValuePair<string, string>($"@{i}", ids[i].ToString()));
            }
            sql += ";";

            return new SqlCommand
            {
                Commandtext = sql,
                Parameters = conditions
            };
        }

        /// <summary>
        /// Creates a command that deletes all mentions of a specific ausbilder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static SqlCommand UpdateTeilnehmer(int id)
        {
            List<KeyValuePair<string, string>> conditions = new List<KeyValuePair<string, string>>();
            conditions.Add(new KeyValuePair<string, string>("@0", id.ToString()));

            string sql = "UPDATE teilnehmer SET fk_ausbilder=0 WHERE fk_ausbilder=@0;";

            return new SqlCommand
            {
                Commandtext = sql,
                Parameters = conditions
            };
        }
        
        /// <summary>
        /// Creates a list of all teilnehmer, including their number of bewerbungen and when the latest was written. 
        /// </summary>
        /// <returns></returns>
        public static List<AngezeigterTeilnehmer> SelectAllTeilnehmer()
        {
            
            List<AngezeigterTeilnehmer> angezeigterTeilnehmer = new List<AngezeigterTeilnehmer>();

            string sql0 = "SELECT t.id, t.vorname, t.name, t.telefon, t.e_mail,";
            sql0 += " b.id, b.bezeichnung,";
            sql0 += " a.ort, a.postleitzahl, a.straße, a.hausnummer, a.land,";
            sql0 += " n.id, nt.nutzertyp,";
            sql0 += " au.id, au.vorname, au.name";
            sql0 += " FROM teilnehmer as t";
            sql0 += " LEFT JOIN beruf b ON t.fk_beruf=b.id";
            sql0 += " LEFT JOIN ausbilder as au ON t.fk_ausbilder=au.id";
            sql0 += " LEFT JOIN adresse as a ON t.fk_adresse=a.id";
            sql0 += " LEFT JOIN nutzer as n ON t.fk_nutzer=n.id";
            sql0 += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp";
            sql0 += " WHERE nt.nutzertyp='Teilnehmer'";

            if (ExecuteSQL(sql0))
            {
                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                {
                    angezeigterTeilnehmer.Add
                    (
                        new AngezeigterTeilnehmer
                        {
                            Id = (int)Result.Tables[0].Rows[i][0],
                            Vorname = $"{Result.Tables[0].Rows[i][1]}",
                            Name = $"{Result.Tables[0].Rows[i][2]}",
                            Telefonnummer = $"{Result.Tables[0].Rows[i][3]}",
                            EMail = $"{Result.Tables[0].Rows[i][4]}",
                            Beruf = new Beruf
                            {
                                Id = (int)Result.Tables[0].Rows[i][5],
                                Bezeichnung = $"{Result.Tables[0].Rows[i][6]}",
                            },
                            Adresse = new Adresse
                            {
                                Ort = $"{Result.Tables[0].Rows[0][7]}",
                                Postleitzahl = $"{Result.Tables[0].Rows[0][8]}",
                                Straße = $"{Result.Tables[0].Rows[0][9]}",
                                Hausnummer = $"{Result.Tables[0].Rows[0][10]}",
                                Land = $"{Result.Tables[0].Rows[0][11]}",
                            },
                            Nutzer = new Nutzer
                            {
                                Id = (int)Result.Tables[0].Rows[0][12],
                                Nutzertyp=new Nutzertyp
                                {
                                    Typ=$"{Result.Tables[0].Rows[0][13]}",
                                }
                            },
                            Ausbilder = new Ausbilder
                            {
                                Id=(int)Result.Tables[0].Rows[0][14],
                                Vorname= $"{Result.Tables[0].Rows[0][15]}",
                                Name=$"{Result.Tables[0].Rows[0][16]}",
                            },
                        }
                    );
                };


                foreach (AngezeigterTeilnehmer Teilnehmer in angezeigterTeilnehmer)
                {
                    string sql = "SELECT gesendet_datum FROM bewerbung WHERE fk_teilnehmer=@0";
                    List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                    parameters.Add(new KeyValuePair<string, string>("@0", Teilnehmer.Id.ToString()));

                    ExecuteSQL(sql, parameters);

                    //Here we add the number of written bewerbungen to the teilnehmer
                    Teilnehmer.Anzahl_Bewerbungen = (int)Result.Tables[0].Rows.Count;

                    List<DateTime> alldates = new List<DateTime>();
                    
                    for(int i=0; i < Teilnehmer.Anzahl_Bewerbungen; i++)
                    {
                        alldates.Add(DateTime.Parse(Result.Tables[0].Rows[i][0].ToString()));
                    }

                    DateTime latestDate = alldates.Where(x => x.Date == alldates.Max(y => y.Date)).FirstOrDefault();
                    Teilnehmer.Letzte_Bewerbung = latestDate;
                }                
            }
            return angezeigterTeilnehmer;
        }


        public static List<Beruf> SelectAllBerufe()
        {
            List<Beruf> AlleBerufe = new List<Beruf>();
            string sql = "SELECT * FROM beruf";

            if(ExecuteSQL(sql))
            {
                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                {
                    AlleBerufe.Add
                    (
                        new Beruf
                        {
                            Id = (int)Result.Tables[0].Rows[i][0],
                            Bezeichnung = Result.Tables[0].Rows[i][1].ToString()
                        }
                    );
                }
            }
            return AlleBerufe;
        }

        public static List<Sicherheitsfrage> SelectAllSicherheitsfragen()
        {
            List<Sicherheitsfrage> AlleSicherheitsfragen = new List<Sicherheitsfrage>();
            string sql = "SELECT * FROM sicherheitsfrage";

            if (ExecuteSQL(sql))
            {
                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                {
                    AlleSicherheitsfragen.Add
                    (
                        new Sicherheitsfrage
                        {
                            Id = (int)Result.Tables[0].Rows[i][0],
                            Frage = Result.Tables[0].Rows[i][1].ToString()
                        }
                    );
                }
            }

            return AlleSicherheitsfragen;
        }

        /// <summary>
        /// Checks whether an E-Mail adress already exists within the database
        /// </summary>
        /// <param name="eMail">The e-mail adress that is checked</param>
        /// <returns>Is unique?</returns>
        public static bool EMailUnique(string eMail)
        {
            string sql = "SELECT";
            sql += "(SELECT COUNT(t.id) FROM teilnehmer as t WHERE t.e_mail = @0 )";
            sql += "+(SELECT COUNT(au.id) FROM ausbilder as au WHERE au.e_mail = @0)";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",eMail),
            };

            if (ExecuteSQL(sql, parameters))
            {
                Int32.TryParse(Result.Tables[0].Rows[0][0].ToString(), out int matches);

                if(matches == 0)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Checks whether or not the query is a simple SELECT or not
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns>Is query?</returns>
        private static bool IsSelectQuery(string sqlCommand)
        {
            string trimmedQuery = sqlCommand.Trim();

            if (trimmedQuery.StartsWith("SELECT"))
            {
                return true;
            }
            return false;
        }
    
        private static void Close()
        {
            connection.Close();
            connection.Dispose();
        }

    }

    /// <summary>
    /// A custom class which helps to structure commands
    /// </summary>
    public class SqlCommand
    {
        public string Commandtext { get; set; }
        public List<KeyValuePair<string, string>> Parameters { get; set; }
    }
}
