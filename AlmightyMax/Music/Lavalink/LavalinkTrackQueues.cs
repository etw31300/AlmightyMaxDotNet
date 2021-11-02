using System;
using System.Collections;
using System.Collections.Generic;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Music.Lavalink
{
    public class LavalinkTrackQueues
    {
        /// <summary>
        /// Registry of all the queues
        /// </summary>
        public static Dictionary<ulong, LinkedList<LavalinkTrack>> QueueRegistry;

        /// <summary>
        /// Initialize the new queue
        /// </summary>
        /// <param name="initDictionary">
        /// Initial dictionary to populate the queue with
        /// </param>
        /// <returns>
        /// QueueRegistry after operation is completed
        /// </returns>
        public static Dictionary<ulong, LinkedList<LavalinkTrack>> Create(Dictionary<ulong, LinkedList<LavalinkTrack>> initDictionary = null)
        {
            if (initDictionary == null || initDictionary.Count == 0)
                QueueRegistry = new Dictionary<ulong, LinkedList<LavalinkTrack>>();
            else
                QueueRegistry = initDictionary;
            return QueueRegistry;
        }

        /// <summary>
        /// Adds a track to the specified guild's queue
        /// </summary>
        /// <param name="guildId">
        /// Id of the guild the queue will be used for
        /// </param>
        /// <param name="track">
        /// LavalinkTrack that will be used to reference the track data for playback
        /// </param>
        /// <returns>
        /// LavalinkTrack that was just added to the queue successfully. Returns null if an error occurred.
        /// </returns>
        public static LavalinkTrack AddTrack(ulong guildId, LavalinkTrack track)
        {
            try
            {
                if (QueueRegistry.ContainsKey(guildId))
                {
                    QueueRegistry[guildId].AddLast(track);
                    return track;
                }
                else
                {
                    QueueRegistry.Add(guildId, new LinkedList<LavalinkTrack>());
                    QueueRegistry[guildId].AddLast(track);
                    return track;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to add track to queue: {track.Uri} - {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the next track from the queue and removes it.
        /// </summary>
        /// <param name="guildId">
        /// Id of the guild used to reference the correct queue
        /// </param>
        /// <returns>
        /// LavalinkTrack that is next in the queue. Returns null if the queue doesn't exist or if there are no songs in
        /// the queue.
        /// </returns>
        public static LavalinkTrack Seek(ulong guildId)
        {
            if (!QueueRegistry.ContainsKey(guildId))
            {
                Console.Error.WriteLine("Queue does not currently exist for this guild.");
                return null;
            }

            var trackQueue = QueueRegistry[guildId];

            if (trackQueue.Count == 0)
                return null;
            
            var track = trackQueue.First?.Value;
            trackQueue.RemoveFirst();
            return track;
        }
    }
}