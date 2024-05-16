using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_TestCSV : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown _ddMenu;

    [SerializeField]
    TextMeshProUGUI _txtTarget;

    private void Awake()
    {
        InitializeTable();
        InitializeDropdown();
    }

    void InitializeTable()
    {
        for (ENTITY_TYPE eType = ENTITY_TYPE.BEGIN + 1; eType < ENTITY_TYPE.END; ++eType)
            Entity.LoadDataBase(eType.FileName(), eType.TypeName());
    }

    void InitializeDropdown()
    {
        List<string> table = new List<string>();

        for (int i = 1; i < (int)ENTITY_TYPE.END; i++)
            table.Add(((ENTITY_TYPE)i).ToString());

        _ddMenu.ClearOptions();
        _ddMenu.AddOptions(table);
        _ddMenu.value = 0;
        _ddMenu.onValueChanged.AddListener(SelectDatatable);
    }

    void SelectDatatable(int index)
    {
        Debug.Log(index);
        _txtTarget.text = string.Empty;
        DataBase dbt = DataBase.dataDBs[((ENTITY_TYPE)index+1).TypeName()];
        string sE = string.Empty;

        for ( int i = 0; i < dbt.row - 1; i++ )
        {
            for ( int j = 0; j < dbt.col; j++ )
            {
                sE = j == 0 ? "" : ( j == ( dbt.col - 1 ) ? "\n" : "\t" );
                _txtTarget.text += dbt.table[i, j] + sE;
            }
        }

    }
}