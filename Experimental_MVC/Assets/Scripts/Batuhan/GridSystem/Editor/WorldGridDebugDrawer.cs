using TMPro;
using UnityEngine;

namespace Batuhan.GridSystem.WorldGrid
{
    public class WorldGridDebugDrawer
    {
        private const float DURATION = 100f;
        private readonly IWorldGridDebuggable _grid;
        private GameObject _debugParent;
        public WorldGridDebugDrawer(IWorldGridDebuggable grid)
        {
            _grid = grid;
        }

        public void DrawDebugLines()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    CreateWorldText($"{x},{y}", _grid.GetWorldPositionCenter(x, y), _grid.Forward);
                    Debug.DrawLine(_grid.GetWorldPosition(x, y), _grid.GetWorldPosition(x, y + 1), Color.white, DURATION);
                    Debug.DrawLine(_grid.GetWorldPosition(x, y), _grid.GetWorldPosition(x + 1, y), Color.white, DURATION);
                }
            }

            Debug.DrawLine(_grid.GetWorldPosition(0, _grid.Height), _grid.GetWorldPosition(_grid.Width, _grid.Height), Color.white, DURATION);
            Debug.DrawLine(_grid.GetWorldPosition(_grid.Width, 0), _grid.GetWorldPosition(_grid.Width, _grid.Height), Color.white, DURATION);
        }

        private void CreateWorldText(string text, Vector3 position, Vector3 direction, int fontSize = 4, Color color = default)
        {
            GameObject textObj = new GameObject("GridText_" + text, typeof(TextMeshPro));
            textObj.transform.position = position + _grid.GetDebugDrawOffset();
            textObj.transform.forward = direction;
            textObj.transform.rotation = Camera.main.transform.rotation;

            TextMeshPro textMesh = textObj.GetComponent<TextMeshPro>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color == default ? Color.cyan : color;
            textMesh.alignment = TextAlignmentOptions.Center;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = 0;

            textObj.transform.SetParent(_debugParent.transform);
        }
        public void ToggleDebug(bool enable)
        {
            if (!enable)
            {
                Cleanup();
            }
            else
            {
                _debugParent = new GameObject($"[{nameof(WorldGridDebugDrawer)}]");
                if (_grid.IsReady)
                {
                    DrawDebugLines();
                }
            }
        }

        public void Cleanup()
        {
            GameObject.Destroy(_debugParent);
        }
    }
}
