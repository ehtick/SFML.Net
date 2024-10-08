using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract base class for streamed audio sources
    /// </summary>
    ////////////////////////////////////////////////////////////
    public abstract class SoundStream : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStream() :
            base(IntPtr.Zero)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Start or resume playing the audio stream.
        ///
        /// This function starts the stream if it was stopped, resumes
        /// it if it was paused, and restarts it from beginning if it
        /// was it already playing.
        /// This function uses its own thread so that it doesn't block
        /// the rest of the program while the stream is played.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Play() => sfSoundStream_play(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pause the audio stream.
        ///
        /// This function pauses the stream if it was playing,
        /// otherwise (stream already paused or stopped) it has no effect.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Pause() => sfSoundStream_pause(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Stop playing the audio stream.
        ///
        /// This function stops the stream if it was playing or paused,
        /// and does nothing if it was already stopped.
        /// It also resets the playing position (unlike pause()).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Stop() => sfSoundStream_stop(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sample rate of the stream
        ///
        /// The sample rate is the number of audio samples played per
        /// second. The higher, the better the quality.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint SampleRate => sfSoundStream_getSampleRate(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Number of channels (1 = mono, 2 = stereo)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint ChannelCount => sfSoundStream_getChannelCount(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current status of the sound stream (see SoundStatus enum)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStatus Status => sfSoundStream_getStatus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Flag if the music should loop after reaching the end.
        ///
        /// If set, the music will restart from beginning after
        /// reaching the end and so on, until it is stopped or
        /// Loop = false is set.
        /// The default looping state for music is false.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool Loop
        {
            get => sfSoundStream_getLoop(CPointer);
            set => sfSoundStream_setLoop(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pitch of the stream.
        /// 
        /// The pitch represents the perceived fundamental frequency
        /// of a sound; thus you can make a sound more acute or grave
        /// by changing its pitch. A side effect of changing the pitch
        /// is to modify the playing speed of the sound as well.
        /// The default value for the pitch is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Pitch
        {
            get => sfSoundStream_getPitch(CPointer);
            set => sfSoundStream_setPitch(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Volume of the stream.
        /// 
        /// The volume is a value between 0 (mute) and 100 (full volume).
        /// The default value for the volume is 100.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Volume
        {
            get => sfSoundStream_getVolume(CPointer);
            set => sfSoundStream_setVolume(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D position of the stream in the audio scene.
        ///
        /// Only sounds with one channel (mono sounds) can be
        /// spatialized.
        /// The default position of a sound is (0, 0, 0).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Position
        {
            get => sfSoundStream_getPosition(CPointer);
            set => sfSoundStream_setPosition(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Make the stream's position relative to the listener or absolute.
        ///
        /// Making a sound relative to the listener will ensure that it will always
        /// be played the same way regardless the position of the listener.
        /// This can be useful for non-spatialized sounds, sounds that are
        /// produced by the listener, or sounds attached to it.
        /// The default value is false (position is absolute).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool RelativeToListener
        {
            get => sfSoundStream_isRelativeToListener(CPointer);
            set => sfSoundStream_setRelativeToListener(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Minimum distance of the music.
        ///
        /// The "minimum distance" of a sound is the maximum
        /// distance at which it is heard at its maximum volume. Further
        /// than the minimum distance, it will start to fade out according
        /// to its attenuation factor. A value of 0 ("inside the head
        /// of the listener") is an invalid value and is forbidden.
        /// The default value of the minimum distance is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float MinDistance
        {
            get => sfSoundStream_getMinDistance(CPointer);
            set => sfSoundStream_setMinDistance(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Attenuation factor of the stream.
        ///
        /// The attenuation is a multiplicative factor which makes
        /// the music more or less loud according to its distance
        /// from the listener. An attenuation of 0 will produce a
        /// non-attenuated sound, i.e. its volume will always be the same
        /// whether it is heard from near or from far. On the other hand,
        /// an attenuation value such as 100 will make the sound fade out
        /// very quickly as it gets further from the listener.
        /// The default value of the attenuation is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Attenuation
        {
            get => sfSoundStream_getAttenuation(CPointer);
            set => sfSoundStream_setAttenuation(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current playing position of the stream.
        /// 
        /// The playing position can be changed when the music is
        /// either paused or playing.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time PlayingOffset
        {
            get => sfSoundStream_getPlayingOffset(CPointer);
            set => sfSoundStream_setPlayingOffset(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            if (IsInvalid)
            {
                return MakeDisposedObjectString();
            }

            return "[SoundStream]" +
                   " SampleRate(" + SampleRate + ")" +
                   " ChannelCount(" + ChannelCount + ")" +
                   " Status(" + Status + ")" +
                   " Loop(" + Loop + ")" +
                   " Pitch(" + Pitch + ")" +
                   " Volume(" + Volume + ")" +
                   " Position(" + Position + ")" +
                   " RelativeToListener(" + RelativeToListener + ")" +
                   " MinDistance(" + MinDistance + ")" +
                   " Attenuation(" + Attenuation + ")" +
                   " PlayingOffset(" + PlayingOffset + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the audio stream parameters, you must call it before Play()
        /// </summary>
        /// <param name="channelCount">Number of channels</param>
        /// <param name="sampleRate">Sample rate, in samples per second</param>
        ////////////////////////////////////////////////////////////
        protected void Initialize(uint channelCount, uint sampleRate)
        {
            _getDataCallback = new GetDataCallbackType(GetData);
            _seekCallback = new SeekCallbackType(Seek);
            CPointer = sfSoundStream_create(_getDataCallback, _seekCallback, channelCount, sampleRate, IntPtr.Zero);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Virtual function called each time new audio data is needed to feed the stream
        /// </summary>
        /// <param name="samples">Array of samples to fill for the stream</param>
        /// <returns>True to continue playback, false to stop</returns>
        ////////////////////////////////////////////////////////////
        protected abstract bool OnGetData(out short[] samples);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Virtual function called to seek into the stream
        /// </summary>
        /// <param name="timeOffset">New position</param>
        ////////////////////////////////////////////////////////////
        protected abstract void OnSeek(Time timeOffset);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfSoundStream_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure mapping the C library arguments passed to the data callback
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        private struct Chunk
        {
            public unsafe short* Samples;
            public uint SampleCount;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called each time new audio data is needed to feed the stream
        /// </summary>
        /// <param name="dataChunk">Data chunk to fill with new audio samples</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>True to continue playback, false to stop</returns>
        ////////////////////////////////////////////////////////////
        private bool GetData(ref Chunk dataChunk, IntPtr userData)
        {
            if (OnGetData(out _tempBuffer))
            {
                unsafe
                {
                    fixed (short* samplesPtr = _tempBuffer)
                    {
                        dataChunk.Samples = samplesPtr;
                        dataChunk.SampleCount = (uint)_tempBuffer.Length;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called to seek in the stream
        /// </summary>
        /// <param name="timeOffset">New position</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>If false is returned, the playback is aborted</returns>
        ////////////////////////////////////////////////////////////
        private void Seek(Time timeOffset, IntPtr userData) => OnSeek(timeOffset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GetDataCallbackType(ref Chunk dataChunk, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SeekCallbackType(Time timeOffset, IntPtr userData);

        private GetDataCallbackType _getDataCallback;
        private SeekCallbackType _seekCallback;
        private short[] _tempBuffer;

        #region Imports
        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfSoundStream_create(GetDataCallbackType onGetData, SeekCallbackType onSeek, uint channelCount, uint sampleRate, IntPtr userData);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_destroy(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_play(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_pause(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_stop(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern SoundStatus sfSoundStream_getStatus(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfSoundStream_getChannelCount(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfSoundStream_getSampleRate(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setLoop(IntPtr soundStream, bool loop);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPitch(IntPtr soundStream, float pitch);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setVolume(IntPtr soundStream, float volume);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPosition(IntPtr soundStream, Vector3f position);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setRelativeToListener(IntPtr soundStream, bool relative);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setMinDistance(IntPtr soundStream, float minDistance);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setAttenuation(IntPtr soundStream, float attenuation);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPlayingOffset(IntPtr soundStream, Time timeOffset);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfSoundStream_getLoop(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getPitch(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getVolume(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSoundStream_getPosition(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfSoundStream_isRelativeToListener(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getMinDistance(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getAttenuation(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfSoundStream_getPlayingOffset(IntPtr soundStream);
        #endregion
    }
}
