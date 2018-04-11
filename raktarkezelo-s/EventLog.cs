using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    sealed class EventLog
    {
        private static readonly EventLog instance = new EventLog();
        public static EventLog Instance
        {
            get { return instance; }
        }
        public LinkedList<ImportNeed> ImportNeeds { get; private set; }
        public LinkedList<Import> Imports { get; private set; }
        public LinkedList<ExportNeed> ExportNeeds { get; private set; }
        public LinkedList<Export> Exports { get; private set; }
        public LinkedList<Moving> Movings { get; private set; }

        private EventLog()
        {
            ImportNeeds = new LinkedList<ImportNeed>();
            Imports = new LinkedList<Import>();
            ExportNeeds = new LinkedList<ExportNeed>();
            Exports = new LinkedList<Export>();
            Movings = new LinkedList<Moving>();
        }

        public void AddImportNeed(ImportNeed importNeed)
        {
            ImportNeeds.AddLast(importNeed);
        }

        public void AddImport(Import import)
        {
            Imports.AddLast(import);
        }

        public void AddExportNeed(ExportNeed exportNeed)
        {
            ExportNeeds.AddLast(exportNeed);
        }

        public void AddExport(Export export)
        {
            Exports.AddLast(export);
        }

        public void AddMoving(Moving moving)
        {
            Movings.AddLast(moving);
        }
    }
}
