using System;
using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	using System.Linq.Expressions;
	using System.Reflection;
	using Mandelbrot.Domain.Fascade;

	public class ChangeParameterSelectionAction<T> : IAction
	{
		private readonly IFractalContext context;

		private readonly PropertyInfo targetProperty;

		private readonly T newValue;

		private T oldValue;

		public ChangeParameterSelectionAction(
			IFractalContext context,
			Expression<Func<IFractalContext, T>> targetPropertyExpression,
			T newValue)
		{
			this.context = context;
			this.targetProperty = ResolveTargetProperty(targetPropertyExpression);
			this.newValue = newValue;
		}

		public string Name => "Change Parameter";

		public virtual bool CanUndo => false;

		public async Task DoAsync()
		{
			await Task.Factory.StartNew(this.Do);
		}

		public async Task UndoAsync()
		{
			await Task.Factory.StartNew(this.Undo);
		}

		private void Do()
		{
			this.oldValue = this.GetTargetPropertyValue();
			this.SetTargetPropertyValue(this.newValue);
		}

		private void Undo()
		{
			this.SetTargetPropertyValue(this.oldValue);
		}

		private T GetTargetPropertyValue()
		{
			return (T)this.targetProperty.GetValue(this.context);
		}

		private void SetTargetPropertyValue(T value)
		{
			this.targetProperty.SetValue(this.context, value);
		}

		private static PropertyInfo ResolveTargetProperty(Expression<Func<IFractalContext, T>> targetPropertyExpression )
		{
			var memberExpression = (MemberExpression)targetPropertyExpression.Body;
			return (PropertyInfo)memberExpression.Member;
		}
	}
}
