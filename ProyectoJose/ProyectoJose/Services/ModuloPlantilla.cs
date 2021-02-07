using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;
using ProyectoJose.Modelo;

namespace ProyectoJose.Services
{
   public class ModuloPlantilla
    {
        public void AddTextToWordDocument(string filepath,  int IdTrabajador, DateTime uno, DateTime dos)
        {
                     //TODO
                    string nombre = "";
                    string dni = "";
                  
                    using (var Context = new PruebaContext())
                    {
                        nombre = Context.Trabajadors.Where(x => x.IdTrabajador == IdTrabajador).First().Nombre;
                        dni = Context.Trabajadors.Where(x => x.IdTrabajador == IdTrabajador).First().Dni;
                       
                    }

                    using (WordprocessingDocument wordDocument = WordprocessingDocument
                        .Create(filepath, WordprocessingDocumentType.Document))
                    {
                        
                        // Add a new main document part. 
                        wordDocument.AddMainDocumentPart();

                        // Create the Document DOM. 
                        wordDocument.MainDocumentPart.Document =
                            new Document(

                          // Create a table.
                          new Table(new TableProperties(new TableStyle { Val = "TableGrid" },
                          new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
                          new TableBorders(new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Birds), Size = 17 },
                            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Birds), Size = 17 },
                            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Birds), Size = 17 },
                            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Birds), Size = 17 })),
                          new TableGrid(new GridColumn()),
                          new TableRow(new TableCell(new Paragraph(new Run(new Text("VACACIONES " +
                          nombre.ToUpper()), new RunProperties(new FontSize() { Val = "25" })))))),

                        new Body(

                                    new Paragraph(
                                        new Run(
                                            new Text(""))),
                                    new Paragraph(
                                        new Run(
                                            new Text(""))),
                                    new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "60" }),
                                        new Run(
                                            new Text("EL TRABAJADOR " + nombre.ToUpper() + " CON DNI/NIE " + dni +
                                            " QUE DESARROLLA SU ACTIVIDAD PROFESIONAL COMO TRABAJADOR " +
                                            " EN LA EMPRESA JOSE ANTONIO, SL RECONOCE QUE: "
                                            ))),

                                       new Paragraph(
                                        new Run(
                                            new Text(""))),
                                         new Paragraph(
                                        new Run(
                                            new Text(""))),
                                           new Paragraph(
                                        new Run(
                                            new Text(""))),

                                    new Paragraph(
                                        new Run(
                                            new Text(
                                           " DISFRUTA, " + OpcionDias(uno, dos) +
                                           ", DE LAS VACACIONES PERTENECIENTES AL AÑO  "
                                            + DateTime.Now.Year))),
                                       new Paragraph(
                                        new Run(
                                            new Text(""))),
                                       new Paragraph(
                                        new Run(
                                            new Text(""))),
                                         new Paragraph(
                                        new Run(
                                            new Text(""))),
                                        new Paragraph(
                                        new Run(
                                            new Text(""))),
                                          new Paragraph(
                                        new Run(
                                            new Text("EN BURGOS A.  "
                                            + DateTime.Now.Day + " DE " + DateTime.Now.Month + " DEL " + DateTime.Now.Year)),

                                         new Paragraph(
                                        new Run(
                                            new Text(""))),

                                          new Paragraph(
                                        new Run(
                                            new Text(""))),
                                          new Paragraph(
                                        new Run(
                                            new Text(""))),

                                         new Paragraph(
                                        new Run(
                                            new Text("FDO.  " + nombre.ToUpper())
                                             )),
                                          new Paragraph(
                                        new Run(
                                            new Text("DNI/NIE: " + dni))))

                                    ));

                        // Save changes to the main document part. 
                        wordDocument.MainDocumentPart.Document.Save();

                    }

        }

        // comprueba si el documento permanece abierto
      public  bool IsFileLocked(string file)
        {
            try
            {
                using (var stream = File.Open(file, FileMode.Open))
                    return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        #region control de fechas  

        public string OpcionDias(DateTime uno, DateTime dos)
        {
            string PeriodoFechas = "";
           
            if (DateTime.Compare(uno, dos) == 0)
            {
                return PeriodoFechas = "El DIA " + uno.ToString("dd/MM/yyyy");
            }
            else
            {
                return PeriodoFechas = "DEL DIA " + uno.ToString("dd/MM/yyyy")
                    + "AL DIA " + dos.ToString("dd/MM/yyyy");
            }

        }


        public string ObtenerFecha(DateTime fecha)
        {
            string fechastring = fecha.ToString("d");
            return fechastring;
        }

        public DateTime ObtenerFechaDate(string fecha)
        {
           DateTime f= DateTime.Parse(fecha);
          return f;
        }


      public  bool ComprobarPeriodo(DateTime uno, DateTime dos, PruebaContext Context, int IdTrabajador)
        {
            bool coincide = false;
            int i = 0;
            var resultado = Context.Periodos.Where(x => x.IdTrabajador == IdTrabajador).ToList();

           // foreach (var item in resultado) // recorremos los periodos del trabajador
           while((!coincide) && (i < resultado.Count)) // pq regla de negación y afirmación
            {                
                // comparamos las fechas finales e iniciales del trabajador con las fechas  seleccionadas 
                if (DateTime.Compare(DateTime.Parse(resultado [i].FechaInicio), dos) > 0 
                    || DateTime.Compare(DateTime.Parse(resultado[i].FechaFin), uno) < 0)
                {
                    coincide = false;

                }
                else
                {
                    coincide = true;
                  
                }
                i++;
            }

            return coincide;
        }

 
        public bool ComprobarAlta(DateTime uno, PruebaContext Context, int IdTrabajador)
        {
            string Falta = Context.Trabajadors.Where(x => x.IdTrabajador == IdTrabajador).First().FechaAlta;

            return DateTime.Compare(DateTime.Parse(Falta), uno) < 0 || DateTime.Compare(DateTime.Parse(Falta), uno)==0;
        }


        public bool CheckFechaEpi(PruebaContext Context, int IdTrabr, DateTime fechaAlta)
        {
            var fechaEpis = Context.TrabajadorEpis.Where(x => x.IdTrabajador == IdTrabr).ToList();
            bool coincide = false;
            int i = 0;

            if (fechaEpis.Count != 0) // controlar si hay fecha de epi
            {

                while ((!coincide) && (i < fechaEpis.Count))
                {
                    if (DateTime.Compare(DateTime.Parse(fechaEpis[i].FechaEpi), fechaAlta) > 0
                       || DateTime.Compare(DateTime.Parse(fechaEpis[i].FechaEpi), fechaAlta) == 0)
                    {
                        coincide = true;

                    }
                    else
                    {
                        return coincide = false;

                    }
                    i++;
                }
            }
            else
            {
                coincide = true;
            }

            return coincide;

        }



        public DateTime CheckfechaModifAlta(PruebaContext Context, int IdTrabr, DateTime fechaAlta)
        {
            var fechasAlta = Context.Periodos.Where(x => x.IdTrabajador == IdTrabr).ToList(); //fecha de los periodos
            var fechaExistente = Context.Trabajadors.Where(x => x.IdTrabajador == IdTrabr).FirstOrDefault().FechaAlta; //fecha guardada
            bool existe = false;
            int a = 0;
            DateTime fechaCorrecta = ObtenerFechaDate(fechaExistente);
            if (fechasAlta.Count != 0)  // controlar si hay periodos
            {
                while ((!existe) && (a < fechasAlta.Count))
                {
                    if (DateTime.Compare(DateTime.Parse(fechasAlta[a].FechaInicio), fechaAlta) > 0
                        || DateTime.Compare(DateTime.Parse(fechasAlta[a].FechaInicio), fechaAlta) == 0)
                    {
                        

                        if (CheckFechaEpi(Context, IdTrabr, fechaAlta))
                        {
                            fechaCorrecta = fechaAlta;

                        }
                        else
                        {
                            return fechaCorrecta = ObtenerFechaDate(fechaExistente);
                        }

                    }
                    else
                    {
                        existe = false;
                        return fechaCorrecta = ObtenerFechaDate(fechaExistente);
                    }
                    a++;
                }
            }
            else
            {

                if (CheckFechaEpi(Context, IdTrabr, fechaAlta))
                {
                    fechaCorrecta = fechaAlta;

                }
                else
                {
                    return fechaCorrecta = ObtenerFechaDate(fechaExistente);
                }

            }

            return fechaCorrecta;

        }





        #endregion
    }
}
