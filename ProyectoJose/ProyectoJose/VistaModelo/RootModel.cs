using ProyectoJose.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProyectoJose.VistaModelo
{
    public class RootModel : INotifyPropertyChanged
    {

        List<Curso> cursoList;
        List<TipoDia> tipoList;
        List<Epi> epiList;
        bool _isStopVisible;
        public List<Curso> CursoList
        {
            get { return cursoList; }
            set
            {
                if (cursoList != value)
                {
                    cursoList = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStopVisible
        {
            get
            {
                return _isStopVisible;
            }
            set
            {
                _isStopVisible = value;
                OnPropertyChanged("IsStopVisible");
            }
        }

        //Curso selectedCurso;
        //public Curso SelectedCurso
        //{
        //    get { return selectedCurso; }
        //    set
        //    {
        //        if (selectedCurso != value)
        //        {
        //            selectedCurso = value;
        //            OnPropertyChanged();
        //        }
        //    }

        //}

        public List<TipoDia> TipoList
        {
            get { return tipoList; }
            set
            {
                if (tipoList != value)
                {
                    tipoList = value;
                    OnPropertyChanged();
                }
            }
        }


        //TipoDia selectedTipo;
        //public TipoDia SelectedTipo
        //{
        //    get { return selectedTipo; }
        //    set
        //    {
        //        if (selectedTipo != value)
        //        {
        //            selectedTipo = value;
        //            OnPropertyChanged();
        //        }
        //    }

        //}


        public List<Epi> EpiList
        {
            get { return epiList; }
            set
            {
                if (epiList != value)
                {
                    epiList = value;
                    OnPropertyChanged();
                }
            }
        }


        //Epi selectedEpi;
        //public Epi SelectedEpi
        //{
        //    get { return selectedEpi; }
        //    set
        //    {
        //        if (selectedEpi != value)
        //        {
        //            selectedEpi = value;
        //            OnPropertyChanged();
        //        }
        //    }

        //}



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
