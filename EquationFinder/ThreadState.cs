using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EquationFinderCore;

namespace EquationFinder
{
	public class ThreadState
	{
		public DateTime TimeToStop;
		public ThreadSpawnerArgs ThreadArgs;
		public CancellationTokenSource CancelToken;

		private ThreadState()
		{ }

		public ThreadState(ThreadSpawnerArgs threadArgs, DateTime timeToStop)
		{
			this.ThreadArgs = threadArgs;
			this.TimeToStop = new DateTime(timeToStop.Ticks);			
			this.CancelToken = new CancellationTokenSource();
		}
	}
}
