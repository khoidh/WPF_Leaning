using System;
using System.Windows;
using System.Windows.Input;

namespace ListBoxBindingObservableCollection.Helpers {
	public class RelayCommand<T>:ICommand {
	  private Action<object> execute;
	  private Action<object,IInputElement> executeWithTarget;
	  private Func<object,bool> canExecute;

	  public event EventHandler CanExecuteChanged {
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	  }

	  public RelayCommand(Action<object> execute,Func<object,bool> canExecute = null) {
		this.execute = execute;
		this.canExecute = canExecute;
	  }

	  public RelayCommand(Action<object,IInputElement> execute,Func<object,bool> canExecute = null) {
		this.executeWithTarget = execute;
		this.canExecute = canExecute;
	  }

	  public bool CanExecute(object parameter) {
		return this.canExecute == null || this.canExecute(parameter);
	  }

	  public void Execute(object parameter) {
		this.execute(parameter);
	  }

	  public void Execute(object parameter,IInputElement target) {
		this.executeWithTarget(parameter,target);
	  }
	}

	public sealed class RelayCommand:ICommand {
	  private Action function;

	  public RelayCommand(Action function) {
		this.function = function;
	  }

	  public bool CanExecute(object parameter) {
		return this.function != null;
	  }

	  public void Execute(object parameter) {
		if(this.function != null) {
		  this.function();
		}
	  }

	  public event EventHandler CanExecuteChanged {
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	  }
	}

	public class DelegateCommand:ICommand {
	  private readonly Action<object> _execute;
	  private readonly Predicate<object> _canExecute;

	  public DelegateCommand(Action<object> execute)
		  : this(execute,null) {
	  }

	  public DelegateCommand(Action<object> execute,Predicate<object> canExecute) {
		if(execute == null)
		  throw new ArgumentNullException("execute");

		_execute = execute;
		_canExecute = canExecute;
	  }

	  public event EventHandler CanExecuteChanged;

	  public bool CanExecute(object parameter) {
		return _canExecute == null ? true : _canExecute(parameter);
	  }

	  public void Execute(object parameter) {
		_execute(parameter);
	  }

	  public void Invalidate() {
		var handler = this.CanExecuteChanged;
		if(handler != null) {
		  handler(this,EventArgs.Empty);
		}
	  }
	}
}
