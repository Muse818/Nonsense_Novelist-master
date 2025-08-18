
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public static class TableManager
{
    public static List<TableBase> Tables = new List<TableBase>();

    public static void Add<T>(T table) where T : TableBase
    {
        Tables.Add(table);
    }

    public static void AddTables()
    {
        Add(new AbilityTable());
        Add(new AbilityTriggerTable());
        Add(new AbilitySelectorTable());
        Add(new AbilityEffectApplierTable());
        Add(new BattleUnitTable());
        Add(new BattleConfig());
        Add(new WordTable());
        Add(new MailTable());
        Add(new LevelTable());
        Add(new PhaseTable());
        Add(new EmitTable());
    }

    public static void ReadTables()
    {
        Debug.Log("----------------------Begin read tables---------------------");
        
        AddTables();
        LoadTables();
        
        Debug.Log("----------------------Finish read tables---------------------");
    }

    public static void LoadTables()
    {
        foreach (var table in Tables)
        {
            table.PreLoad();
            
            var meta = table.Load();
            
            if (meta.mErrorType == TableErrorType.None)
            {
                table.PostLoad(); 
            }
            
            // print table load info
            switch (meta.mErrorType)
            {
                case TableErrorType.None:
                    Debug.Log($"Load table {table.AssetName} success!");
                    break;
                
                case TableErrorType.NotExist:
                    Debug.LogError($"Not Find {table.AssetName}!");
                    break;
                
                case TableErrorType.ParseLine:
                    Debug.LogError($"Failed to parse table {table.AssetName} in line {meta.row} col {meta.col}!");
                    break;
                
                case TableErrorType.Duplicate:
                    Debug.LogError($"table {table.AssetName} has duplicate key in line {meta.row}!");
                    break;
            }
        }
    }
}

