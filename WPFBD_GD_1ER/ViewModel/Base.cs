using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace WPFBD_GD_1ER.ViewModel
{
    public class BasePropriete : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }

        protected bool AssignerChamp<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            PropertyChangedEventHandler handler = PropertyChanged;
            field = value;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName.Substring(4)));
            //OnPropertyChanged();
            return true;
        }
    }

    public class BaseCommande : ICommand
    {
        private bool lParam = false;
        private Action _Action;
        private Action<object> _ActionObject;

        public BaseCommande(Action Action_)
        { _Action = Action_; }

        public BaseCommande(Action<object> ActionObject_)
        { _ActionObject = ActionObject_; lParam = true; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        { return true; }

        public void Execute(object parameter)
        {
            if (lParam)
            {
                if (_ActionObject != null)
                    _ActionObject(parameter);
            }
            else
            {
                if (_Action != null)
                    _Action();
            }
        }

    }
}