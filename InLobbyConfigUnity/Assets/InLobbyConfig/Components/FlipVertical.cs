using UnityEngine;

namespace InLobbyConfig.Components
{
    public class FlipVertical : MonoBehaviour
    {
        public void Flip()
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }
    }
}
