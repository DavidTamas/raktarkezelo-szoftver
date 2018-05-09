using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    sealed class EventContainer
    {
        private static readonly EventContainer instance = new EventContainer();
        public static EventContainer Instance
        {
            get { return instance; }
        }
        public List<ImportNeed> ImportNeeds { get; private set; }
        public List<Import> Imports { get; private set; }
        public List<ExportNeed> ExportNeeds { get; private set; }
        public List<Export> Exports { get; private set; }
        public List<Moving> Movings { get; private set; }

        private EventContainer()
        {
            ImportNeeds = new List<ImportNeed>();
            Imports = new List<Import>();
            ExportNeeds = new List<ExportNeed>();
            Exports = new List<Export>();
            Movings = new List<Moving>();
        }

        public void AddImportNeed(ImportNeed importNeed)
        {
            ImportNeeds.Add(importNeed);
        }

        public void AddImport(Import import)
        {
            Imports.Add(import);
        }

        public void AddExportNeed(ExportNeed exportNeed)
        {
            ExportNeeds.Add(exportNeed);
        }

        public void AddExport(Export export)
        {
            Exports.Add(export);
        }

        public void AddMoving(Moving moving)
        {
            Movings.Add(moving);
        }

        public void PrintImportNeeds()
        {
            Console.WriteLine("-- Import Needs: --");
            foreach(var need in ImportNeeds)
            {
                need.Print();
            }
        }

        public void PrintImports()
        {
            Console.WriteLine("-- Imports: --");
            foreach (var import in Imports)
            {
                import.Print();
            }
        }
    }
}
