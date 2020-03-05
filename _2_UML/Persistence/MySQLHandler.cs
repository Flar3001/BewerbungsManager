using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2_UML.Models;

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

        //This is the result of sql-SELECT-queries. Must be cleared before a new query is executed
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
                
                bool isSelect = CheckQuery(sqlString);

                try
                {
                    newCommand.Prepare();
                    newCommand.ExecuteNonQuery();

                    if (isSelect)
                    {
                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(newCommand);
                        dataAdapter.Fill(Result);

                        if (CheckResults() == false)
                        {
                            Errormessage = "Es konnten keine Daten aus der Datenbank ausgelesen werden";
                            everythingOkay = false;
                        }
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

        public static bool UpdateTeilnehmer(Teilnehmer teilnehmer)
        {
            if (UpdateAdresse(teilnehmer.Adresse))
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
                    //new KeyValuePair<string, string>("@7",teilnehmer.Nutzer.Id.ToString()),
                    new KeyValuePair<string, string>("@8",teilnehmer.Id.ToString()),
                };

                if (ExecuteSQL(sql, parameters))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool UpdateAdresse(Adresse adresse)
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

            if (ExecuteSQL(sql, parameters))
            {
                return true;
            }
            return false;
        }

        public static bool AddNewAusbilder(Ausbilder ausbilder)
        {
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


        private static bool NeuesteAdresseAuswaehlen()
        {
            string sql = "SELECT MAX(a.id) FROM adresse as a;";
            if (ExecuteSQL(sql))
            {
                return true;
            }
            return false;
        }

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
        

        public static bool CheckLogin(string email, string password)
        {
            CheckLoginTeilnehmer(email, password);

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
            Application.Current.Properties.Clear();

            Application.Current.Properties.Add("User_Vorname", $"{Result.Tables[0].Rows[0][0]}");
            Application.Current.Properties.Add("User_Nachname", $"{Result.Tables[0].Rows[0][1]}");
            Application.Current.Properties.Add("User_Telefone", $"{Result.Tables[0].Rows[0][2]}");
            Application.Current.Properties.Add("User_E_Mail", $"{Result.Tables[0].Rows[0][3]}");
            Application.Current.Properties.Add("User_Nutzertyp", $"{Result.Tables[0].Rows[0][4]}");
            Application.Current.Properties.Add("User_Id", $"{ Result.Tables[0].Rows[0][5]}");

            if (Result.Tables[0].Rows[0][4].ToString() == "Teilnehmer")
            {
                Application.Current.Properties.Add("User_Beruf", $"{Result.Tables[0].Rows[0][6]}");
                Application.Current.Properties.Add("User_Wohnort", $"{Result.Tables[0].Rows[0][7]}");
                Application.Current.Properties.Add("User_Postleitzahl", $"{Result.Tables[0].Rows[0][8]}");
                Application.Current.Properties.Add("User_Straße", $"{Result.Tables[0].Rows[0][9]}");
                Application.Current.Properties.Add("User_Hausnummer", $"{Result.Tables[0].Rows[0][10]}");
                Application.Current.Properties.Add("User_Land", $"{Result.Tables[0].Rows[0][11]}");
                Application.Current.Properties.Add("User_Ausbilder_Vorname", $"{Result.Tables[0].Rows[0][12]}");
                Application.Current.Properties.Add("User_Ausbilder_Nachname", $"{Result.Tables[0].Rows[0][13]}");
                Application.Current.Properties.Add("User_Ausbilder_Id", $"{Result.Tables[0].Rows[0][14]}");
            }
        }


        /// <summary>
        /// Durchsucht nch einem Teilnehmer mit den eingegebenen Anmeldedaten
        /// </summary>
        /// <param name="email">Eingebene E-Mail-Adresse</param>
        /// <param name="password">Eingegebenes Passwort</param>
        private static void CheckLoginTeilnehmer(string email, string password)
        {
            string sql = "SELECT t.vorname, t.name, t.telefon, t.e_mail, nt.nutzertyp, n.id,";
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

            ExecuteSQL(sql, parameters);
        }


        /// <summary>
        /// Durchsucht nch einem Ausbilder mit den eingegebenen Anmeldedaten
        /// </summary>
        /// <param name="email">Eingebene E-Mail-Adresse</param>
        /// <param name="password">Eingegebenes Passwort</param>
        private static void CheckLoginAusbilder(string email, string password)
        {
            string sql = "SELECT au.vorname, au.name, au.telefon, au.e_mail, nt.nutzertyp, n.id";

            sql += " FROM ausbilder as au";
            sql += " LEFT JOIN nutzer as n ON au.fk_nutzer=n.id";
            sql += " LEFT JOIN nutzertyp as nt ON n.fk_nutzertyp=nt.id";
            sql += " WHERE au.e_mail = @0 AND n.passwort = @1;";


            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",email),
                new KeyValuePair<string, string>("@1",password),
            };

            ExecuteSQL(sql, parameters);
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
        
        public static List<Ausbilder> SelectAllAusbilder()
        {
            List<Ausbilder> AllAusbilder = new List<Ausbilder>();

            string sql = "SELECT au.id, au.vorname, au.name, au.telefon, au.e_mail";
            sql += ", n.id";
            sql += " FROM ausbilder as au";
            sql += " LEFT JOIN nutzer as n ON au.fk_nutzer=n.id";


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
                            Nutzer = new Nutzer { Id = (int)Result.Tables[0].Rows[i][5] }
                        }
                    );
                }
            }

            return AllAusbilder;            
        }

        public static void DeleteFromAusbilder(int deleteid)
        {
            string sql = "DELETE FROM ausbilder";
            sql += "WHERE id=@0";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("@0",deleteid.ToString()),
            };

            ExecuteSQL(sql, parameters);
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
        private static bool CheckQuery(string sqlCommand)
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
}
