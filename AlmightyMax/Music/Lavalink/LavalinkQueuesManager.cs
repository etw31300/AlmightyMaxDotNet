using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Music.Lavalink
{
    public class LavalinkQueuesManager
    {
        /// <summary>
        /// Registry of all the queues
        /// </summary>
        private static Dictionary<ulong, LinkedList<LavalinkTrack>> _queueRegistry;

        /// <summary>
        /// Initialize the new queue
        /// </summary>
        /// <param name="initDictionary">
        /// Initial dictionary to populate the queue with
        /// </param>
        /// <returns>
        /// QueueRegistry after operation is completed
        /// </returns>
        public static async Task Create(Dictionary<ulong, LinkedList<LavalinkTrack>> initDictionary = null)
        {
            if (initDictionary == null || initDictionary.Count == 0)
                _queueRegistry = new Dictionary<ulong, LinkedList<LavalinkTrack>>();
            else
                _queueRegistry = initDictionary;
            
            await Console.Out.WriteLineAsync("Queue registry created successfully.");
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
        public static async Task<LavalinkTrack> AddTrack(ulong guildId, LavalinkTrack track)
        {
            try
            {
                if (_queueRegistry.ContainsKey(guildId))
                {
                    _queueRegistry[guildId].AddLast(track);
                    return track;
                }
                else
                {
                    _queueRegistry.Add(guildId, new LinkedList<LavalinkTrack>());
                    _queueRegistry[guildId].AddLast(track);
                    return track;
                }
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync($"Failed to add track to queue: {track.Uri} - {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the next track from the queue and removes it. Returns null if the queue doesn't exist or if there are
        /// no songs in the queue.
        /// </summary>
        /// <param name="guildId">
        /// Id of the guild used to reference the correct queue
        /// </param>
        /// <returns>
        /// LavalinkTrack that is next in the queue. 
        /// </returns>
        public static async Task<LavalinkTrack> Seek(ulong guildId)
        {
            if (!_queueRegistry.ContainsKey(guildId))
            {
                await Console.Error.WriteLineAsync("Queue does not currently exist for this guild.");
                return null;
            }

            var trackQueue = _queueRegistry[guildId];

            if (trackQueue.Count == 0)
                return null;
            
            var track = trackQueue.First?.Value;
            trackQueue.RemoveFirst();
            return track;
        }

        /// <summary>
        /// Gets the next track from the queue without removing it. Returns null if the queue doesn't exist or if there
        /// are no songs in the queue.
        /// </summary>
        /// <param name="guildId">
        /// Id of the guild used to reference the correct queue
        /// </param>
        /// <returns>
        /// LavalinkTrack that is next in the queue
        /// </returns>
        public static async Task<LavalinkTrack> Peek(ulong guildId)
        {
            if (!_queueRegistry.ContainsKey(guildId))
            {
                await Console.Error.WriteLineAsync("Queue does not currently exist for this guild.");
                return null;
            }

            var trackQueue = _queueRegistry[guildId];

            return trackQueue.Count == 0 ? null : trackQueue.First?.Value;
        }

        public static async Task ClearQueue(ulong guildId)
        {
            if (!_queueRegistry.ContainsKey(guildId))
                return;

            _queueRegistry.Remove(guildId);
        }
    }
}