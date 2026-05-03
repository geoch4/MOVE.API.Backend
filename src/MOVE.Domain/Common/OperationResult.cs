using System;
using System.Collections.Generic;
using System.Text;
using MOVE.Domain.Common;

namespace MOVE.Domain.Common
{
		public class OperationResult
		{
			protected OperationResult()
			{
				this.Success = true;
			}
			protected OperationResult(string message)
			{
				this.Success = false;
				this.FailureMessage = message;
			}
			protected OperationResult(Exception ex)
			{
				this.Success = false;
				this.Exception = ex;
			}
			public bool Success { get; protected set; }
			public string FailureMessage { get; protected set; }
			public Exception Exception { get; protected set; }
			public static OperationResult SuccessResult()
			{
				return new OperationResult();
			}
			public static OperationResult FailureResult(string message)
			{
				return new OperationResult(message);
			}
			public static OperationResult ExceptionResult(Exception ex)
			{
				return new OperationResult(ex);
			}
			public bool IsException()
			{
				return this.Exception != null;
			}
		}
	}

public class OperationResult<T> : OperationResult
{
	public T Data { get; private set; }

	// Lägg till dina factory-metoder här
	private OperationResult(T data)
	{
		this.Data = data;
		this.Success = true;
	}

	private OperationResult(string message) : base(message)
	{
	}

	private OperationResult(Exception ex) : base(ex)
	{
	}

	public static OperationResult<T> SuccessResult(T data)
	{
		return new OperationResult<T>(data);
	}

	public static OperationResult<T> FailureResult(string message)
	{
		return new OperationResult<T>(message);
	}

	public static OperationResult<T> ExceptionResult(Exception ex)
	{
		return new OperationResult<T>(ex);
	}
}


