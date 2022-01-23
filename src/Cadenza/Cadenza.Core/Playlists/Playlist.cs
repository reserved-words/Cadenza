﻿namespace Cadenza.Core;

public class Playlist : IPlaylist
{
    private Stack<BasicTrack> _played;
    private BasicTrack _playing;
    private Stack<BasicTrack> _toPlay;
    private List<BasicTrack> _allTracks;

    private readonly PlaylistDefinition _definition;

    public BasicTrack Current => _playing;

    public Playlist(PlaylistDefinition def)
    {
        _definition = def;

        _allTracks = def.Tracks.ToList();

        _played = new Stack<BasicTrack>(_allTracks);
        _playing = _allTracks[0];
        _toPlay = new Stack<BasicTrack>(_allTracks);
    }

    public bool CurrentIsLast => _toPlay.Count == 0;

    public PlaylistType Type => _definition.Type;
    public string Name => _definition.Name;

    public async Task<BasicTrack> MoveNext()
    {
        _played.Push(_playing);

        _playing = _toPlay.Count == 0
            ? null
            : _toPlay.Pop();

        return _playing;
    }

    public async Task<BasicTrack> MovePrevious()
    {
        if (_played.Count > 0)
        {
            if (_playing != null)
            {
                _toPlay.Push(_playing);
            }
            _playing = _played.Pop();
        }

        return _playing;
    }
}