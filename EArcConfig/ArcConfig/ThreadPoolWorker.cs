/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 29.03.2021
 * Time: 20:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace ArcConfig
{
	/// <summary>
	/// Description of ThreadPoolWorker.
	/// </summary>
	public class ThreadPoolWorker
	{
		public ThreadPoolWorker()
		{
		}
		
        private readonly Action<object> action;

        public ThreadPoolWorker(Action<object> action)
        {
        	this.action = action ; // ?? new ArgumentNullException(action);
        }

        public bool Success { get; private set; } //= false;
        public bool Completed { get; private set; } //= false;
        public Exception Exception { get; private set; } //= null;

        public void Start(object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
        }

        public void Wait()
        {
            while (Completed == false)
            {
                Thread.Sleep(150);
            }

            if(Exception != null)
            {
                throw Exception;
            }
        }

        private void ThreadExecution(object state)
        {
            try
            {
                action.Invoke(state);
                Success = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                Success = false;
            }
            finally
            {
                Completed = true;
            }
        }
		
		
		
		
	}
}
