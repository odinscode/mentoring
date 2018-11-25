using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SystemWatcherSolution.Services
{
    public class CustomFileSystemWatcher : FileSystemWatcher
    {
        public List<Regex> RegexPatterns { get; set; } = new List<Regex>();

        #region events
        /// <summary>
        /// Occurs when [changed].
        /// </summary>
        public new event FileSystemEventHandler Changed
        {
            add
            {
                IsChanged += value;
                base.Changed += CustomFileSystemWatcher_Changed;
            }
            remove
            {
                IsChanged -= value;
                base.Created -= CustomFileSystemWatcher_Changed;
            }
        }

        /// <summary>
        /// Occurs when [created].
        /// </summary>
        public new event FileSystemEventHandler Created
        {
            add
            {
                IsCreated += value;
                base.Created += CustomFileSystemWatcher_Created;
            }
            remove
            {
                IsCreated -= value;
                base.Created -= CustomFileSystemWatcher_Created;
            }
        }

        /// <summary>
        /// Occurs when [deleted].
        /// </summary>
        public new event FileSystemEventHandler Deleted
        {
            add
            {
                IsDeleted += value;
                base.Deleted += CustomFileSystemWatcher_Deleted;
            }
            remove
            {
                IsDeleted -= value;
                base.Deleted -= CustomFileSystemWatcher_Deleted;
            }
        }

        /// <summary>
        /// Occurs when [renamed].
        /// </summary>
        public new event RenamedEventHandler Renamed
        {
            add
            {
                IsRenamed += value;
                base.Renamed += CustomFileSystemWatcher_Renamed;
            }
            remove
            {
                IsRenamed -= value;
                base.Renamed -= CustomFileSystemWatcher_Renamed;
            }
        }

        /// <summary>
        /// Occurs when [is changed].
        /// </summary>
        private event FileSystemEventHandler IsChanged;

        /// <summary>
        /// Occurs when [is created].
        /// </summary>
        private event FileSystemEventHandler IsCreated;

        /// <summary>
        /// Occurs when [is deleted].
        /// </summary>
        private event FileSystemEventHandler IsDeleted;

        /// <summary>
        /// Occurs when [is renamed].
        /// </summary>
        private event RenamedEventHandler IsRenamed;
        #endregion

        #region handlers
        /// <summary>
        /// Handles the Changed event of the CustomFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> 
        /// instance containing the event data.</param>
        private void CustomFileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (RegexPatterns.Count == 0)
                IsChanged?.Invoke(sender, e);
            else if (MatchesRegex(e.Name))
                IsChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the Created event of the CustomFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> 
        /// instance containing the event data.</param>
        private void CustomFileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (RegexPatterns.Count == 0)
                IsCreated?.Invoke(sender, e);
            else if (MatchesRegex(e.Name))
                IsCreated?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the Deleted event of the CustomFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> 
        /// instance containing the event data.</param>
        private void CustomFileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (RegexPatterns.Count == 0)
                IsDeleted?.Invoke(sender, e);
            else if (MatchesRegex(e.Name))
                IsDeleted?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the Renamed event of the CustomFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenamedEventArgs"/> 
        /// instance containing the event data.</param>
        private void CustomFileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (RegexPatterns.Count == 0)
                IsRenamed?.Invoke(sender, e);
            else if (MatchesRegex(e.Name))
                IsRenamed?.Invoke(sender, e);
        }

        /// <summary>
        /// Matches the regex.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if regex matches the file, <c>false</c> otherwise.</returns>
        private bool MatchesRegex(string file)
        {
            return RegexPattern.IsMatch(file);
        }
        #endregion

        #region Constructors
        public CustomFileSystemWatcher() : base() { }

        public CustomFileSystemWatcher(string path) : base(path) { }

        public CustomFileSystemWatcher(string path, string filter) : base(path, filter) { }

        public CustomFileSystemWatcher(string path, Regex pattern) : base(path)
        {
            if (!this.RegexPatterns.Contains(pattern))
                this.RegexPatterns.Add(pattern);
        }

        public CustomFileSystemWatcher(string path, List<Regex> patterns) : base(path)
        {
            this.RegexPatterns = patterns;
        }
        #endregion
    }
}