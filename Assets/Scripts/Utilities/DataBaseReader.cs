using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class DataBaseReader
{
    public static void LoadStringToDataTable(string path, DataBase db)
    {
        try
        {
            path = $"{ComType.DATA_PATH}/{path}";
            TextAsset datatable = Resources.Load<TextAsset>(path);

            if (datatable == null)
            {
                GameManager.Log(path + " 파일 읽기 실패", "red");
                return;
            }

            string[] lines = datatable.text.Split('\r');
            string[] columnName = lines[0].Split(',');
            string[] columns;

            int iValue;
			long lValue;
            float fValue;

            db.table = new DataValue[lines.Length - 1, columnName.Length];

            for (int l = 1, ll = lines.Length, r = 0; l < ll; ++l, ++r)
            {
                if (lines[l].LastIndexOf('\r') >= 0)
                {
                    lines[l] = lines[l].Substring(0, lines[l].Length - 1);
                }

                columns = ComUtil.SplitCsvLine(lines[l]);

                for (int c = 0, cc = columnName.Length; c < cc; ++c)
                {
                    if (c >= columns.Length) db.table[r, c] = 0;
                    else if (int.TryParse(columns[c], out iValue))
                    {
                        db.table[r, c] = iValue;
                    }
					else if (long.TryParse(columns[c], out lValue))
					{
						db.table[r, c] = lValue;
					}
                    else if (float.TryParse(columns[c], out fValue))
                    {
                        db.table[r, c] = fValue;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(columns[c]))
                        {
                            db.table[r, c] = string.Empty;
                        }
                        else
                        {
                            if (columns[c][0] == '\"' && columns[c][columns.Length - 1] == '\"')
                                columns[c] = columns[c].Substring(1, columns[c].Length - 2);

                            columns[c] = columns[c].TrimStart('\n');

                            db.table[r, c] = columns[c];
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            GameManager.Log(e.ToString(), "red");
        }
    }
}
