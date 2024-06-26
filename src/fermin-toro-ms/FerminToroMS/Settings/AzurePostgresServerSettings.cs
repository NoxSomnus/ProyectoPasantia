﻿using System.Diagnostics.CodeAnalysis;

namespace FerminToroMS.Settings;


public class AzurePostgresServerSettings
{
    public string Host { get; set; }

    public string Database { get; set; }

    public string Username { get; set; }

    public string Passfile { get; set; }
}
