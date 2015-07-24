﻿using System;
using System.Web;
using System.Linq;
using SmartStore.Core;
using SmartStore.Services.Stores;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Threading;
using SmartStore.Core.Domain.Tasks;

namespace SmartStore.Services.Tasks
{
    /// <summary>
    /// Task scheduler interface
    /// </summary>
	public interface ITaskScheduler
    {
        /// <summary>
        /// The interval in which the scheduler triggers the sweep url 
		/// (which determines pending tasks and executes them in the scope of a regular HTTP request).
        /// </summary>
		TimeSpan SweepInterval { get; set; }

		/// <summary>
		/// The fully qualified base url
		/// </summary>
        string BaseUrl { get; set; }

		/// <summary>
		///  Gets a value indicating whether the scheduler is active and periodically sweeps all tasks.
		/// </summary>
        bool IsActive { get; }

		/// <summary>
		/// Gets a value indicating whether a task is currently running
		/// </summary>
		/// <param name="scheduleTaskId">A <see cref="ScheduleTask"/> identifier</param>
		/// <returns><c>true</c> if the task is running, <c>false</c> otherwise</returns>
		bool IsTaskRunning(int scheduleTaskId);

		/// <summary>
		/// Gets a sequence of <see cref="TaskProgressInfo"/> instances containing 
		/// metadata and progress information about currently running tasks.
		/// </summary>
		/// <returns>A sequence of task info objects if any task is running, an empty enumerable otherwise.</returns>
		IEnumerable<TaskProgressInfo> GetAllRunningTasks();

		/// <summary>
		/// Gets a <see cref="TaskProgressInfo"/> instance containing 
		/// metadata and progress information about a currently running task.
		/// </summary>
		/// <param name="scheduleTaskId">A <see cref="ScheduleTask"/> identifier</param>
		/// <returns>A task info object if the task is running, <c>null</c> otherwise</returns>
		TaskProgressInfo GetRunningTask(int scheduleTaskId);

		/// <summary>
		/// Gets a <see cref="CancellationTokenSource"/> instance used 
		/// to signal a task cancellation request.
		/// </summary>
		/// <param name="scheduleTaskId">A <see cref="ScheduleTask"/> identifier</param>
		/// <returns>A <see cref="CancellationTokenSource"/> instance if the task is running, <c>null</c> otherwise</returns>
		CancellationTokenSource GetCancelTokenSourceFor(int scheduleTaskId);

		/// <summary>
		/// Starts/initializes the scheduler
		/// </summary>
        void Start();

		/// <summary>
		/// Stops the scheduler
		/// </summary>
        void Stop();

		/// <summary>
		/// Executes a single task immediately
		/// </summary>
		/// <param name="scheduleTaskId"></param>
        void RunSingleTask(int scheduleTaskId);

		/// <summary>
		/// Verifies the authentication token which is generated right before the HTTP endpoint gets called.
		/// </summary>
		/// <param name="authToken">The authentication token to verify</param>
		/// <returns><c>true</c> if the validation succeeds, <c>false</c> otherwise</returns>
		/// <remarks>
		/// The task scheduler sends the token as a HTTP request header item.
		/// The called endpoint (e.g. a controller action) is reponsible for invoking
		/// this method and quitting the tasks's execution - preferrably with HTTP 403 -
		/// if the verification fails.
		/// </remarks>
        bool VerifyAuthToken(string authToken);
    }

    public static class ITaskSchedulerExtensions
    {
		///// <param name="taskType">The type of the task (must implement <see cref="ITask"/>)</param>
		//public static bool IsTaskRunning<TTask>(this ITaskScheduler scheduler) where TTask : ITask
		//{
		//	return scheduler.IsTaskRunning(typeof(TTask));
		//}

		//public static TaskProgressInfo GetRunningTask<TTask>(this ITaskScheduler scheduler) where TTask : ITask
		//{
		//	return scheduler.GetRunningTask(typeof(TTask));
		//}

		//public static CancellationTokenSource GetCancelTokenSourceFor<TTask>(this ITaskScheduler scheduler) where TTask : ITask
		//{
		//	return scheduler.GetCancelTokenSourceFor(typeof(TTask));
		//}
		
		internal static void SetBaseUrl(this ITaskScheduler scheduler, IStoreService storeService, HttpContextBase httpContext)
        {
            var path = VirtualPathUtility.ToAbsolute("~/TaskScheduler");
            string url = "";

            if (!httpContext.Request.IsLocal)
            {
                var defaultStore = storeService.GetAllStores().FirstOrDefault(x => storeService.IsStoreDataValid(x));
                if (defaultStore != null)
                {
                    url = defaultStore.Url;
                }
            }

            if (url.IsEmpty())
            {
                url = WebHelper.GetAbsoluteUrl(path, httpContext.Request);
            }

            scheduler.BaseUrl = url;
        }
    }
}
