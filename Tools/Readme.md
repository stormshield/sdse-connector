# Généralités

## Introduction
Les kits de démonstration sont fournis à titre d'exemple sous forme de solution Visual Studio 2017, l'utilisation est la modification est libre, de ce fait Stormshield n'assurera aucun support en cas de mauvais fonctionnement.

## Prérequis
Pour utiliser ces kits il est nécessaire de disposer d'une licence Connector valide ainsi que d'un compte SDSE connecté.

## SDConnectorUseCaseAddIn
Ce kit permet de chiffrer une pièce jointe lors de l'envoi d'un mail.

### Dépendence
```
Accessibility
Microsoft.Office.Interop.Outlook
Microsoft.Office.Tools
Microsoft.Office.Tools.Common
Microsoft.VisualStudio.Tools.Applications.Runtime
stdole
System
System.Data
System.Drawing
System.Windows.Forms
System.Xml
System.Core
System.Xml.Linq
System.Data.DataSetExtensions
Microsoft.CSharp
Microsoft.Office.Tools.v4.0.Framework
Microsoft.Office.Tools.Outlook
Microsoft.Office.Tools.Common.v4.0.Utilities
Microsoft.Office.Tools.Outlook.v4.0.Utilities
```


## SDConnectorUseCaseCRM
Ce kit permet de crée un mail contenant des pièces jointe chiffrés.

### Dépendence
```
Microsoft.CSharp
System
System.Drawing
System.Windows.Forms
```

## SDConnectorUseCaseMonitor
Ce kit permet de monitorer un dossier afin de chiffrer son contenu via le module File.

### Dépendence
```
System
System.Core
System.Xml.Linq
System.Data.DataSetExtensions
Microsoft.CSharp
System.Data
System.Deployment
System.Drawing
System.Windows.Forms
System.Xml
```

## SDConnectorUseCaseMonitorAIP
Ce kit permet de monitorer un dossier afin de chiffrer son contenu via le module File en fonction de d'une classification AIP.

### Dépendence
```
Microsoft.PowerShell.Activities
Microsoft.PowerShell.Commands.Management
Microsoft.PowerShell.Commands.Utility
Microsoft.PowerShell.ConsoleHost
Microsoft.PowerShell.Core.Activities
Microsoft.PowerShell.Diagnostics.Activities
Microsoft.PowerShell.Management.Activities
Microsoft.PowerShell.ScheduledJob
Microsoft.PowerShell.Security
Microsoft.PowerShell.Security.Activities
Microsoft.PowerShell.Utility.Activities
Microsoft.PowerShell.Workflow.ServiceCore
Microsoft.QualityTools.Testing.Fakes
Microsoft.WSMan.Management.Activities
```

## SDConnectorUseCaseNewEMail
Ce kit permet d'envoyer des mail signé/chiffré.

### Dépendence
```
System
System.Core
System.Xml.Linq
System.Data.DataSetExtensions
Microsoft.CSharp
System.Data
System.Deployment
System.Drawing
System.Windows.Forms
System.Xml
```

## SDConnectorUseCaseTeamRuleCreator
Ce kit permet de créer des règles Team basé sur les ACL.

### Dépendence
```
System
System.configuration
System.Core
System.DirectoryServices
System.DirectoryServices.AccountManagement
System.Management
System.Xml.Linq
System.Data.DataSetExtensions
Microsoft.CSharp
System.Data
System.Deployment
System.Drawing
System.Windows.Forms
System.Xml
```
