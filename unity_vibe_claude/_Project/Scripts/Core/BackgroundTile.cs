using UnityEngine;

/// <summary>
/// 플레이어 주변에 반복 배경을 생성합니다.
/// </summary>
public class BackgroundTile : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private SpriteRenderer _tilePrefab;
    [SerializeField] private int _gridSize = 5;
    [SerializeField] private float _tileSize = 10f;
    [SerializeField] private Color _colorA = new Color(0.15f, 0.15f, 0.15f);
    [SerializeField] private Color _colorB = new Color(0.12f, 0.12f, 0.12f);

    private SpriteRenderer[,] _tiles;
    private Vector2Int _lastGridPos;

    private void Start()
    {
        _tiles = new SpriteRenderer[_gridSize, _gridSize];

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                var tile = Instantiate(_tilePrefab, transform);
                tile.color = (x + y) % 2 == 0 ? _colorA : _colorB;
                _tiles[x, y] = tile;
            }
        }

        UpdateTiles();
    }

    private void LateUpdate()
    {
        Vector2Int currentGrid = new Vector2Int(
            Mathf.FloorToInt(_player.position.x / _tileSize),
            Mathf.FloorToInt(_player.position.y / _tileSize)
        );

        if (currentGrid != _lastGridPos)
        {
            UpdateTiles();
            _lastGridPos = currentGrid;
        }
    }

    private void UpdateTiles()
    {
        int half = _gridSize / 2;
        int centerX = Mathf.FloorToInt(_player.position.x / _tileSize);
        int centerY = Mathf.FloorToInt(_player.position.y / _tileSize);

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                int worldX = centerX + x - half;
                int worldY = centerY + y - half;

                _tiles[x, y].transform.position = new Vector3(
                    worldX * _tileSize + _tileSize / 2f,
                    worldY * _tileSize + _tileSize / 2f,
                    1f // 뒤에 깔리도록
                );

                _tiles[x, y].color = (worldX + worldY) % 2 == 0 ? _colorA : _colorB;
            }
        }
    }
}