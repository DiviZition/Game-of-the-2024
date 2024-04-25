using System.Collections;
using UnityEngine;

public class ExplosionMaker : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private ParticleSystem[] _vfxParts;

    [SerializeField] private Transform _petardaVisualHolder;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _VFXSizeMultiplier;
    [SerializeField] private int _damage;

    [SerializeField] private float _destroyTimeAfterExplosion;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        FreezePetardasBody();
        PlayVFX();

        DoDamageToAllInArea();
        StartCoroutine(SelfDestructTimer());
    }

    private void FreezePetardasBody()
    {
        MeshRenderer[] renderers = _petardaVisualHolder.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        _transform.rotation = Quaternion.identity;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    private void PlayVFX()
    {
        for (int i = 0; i < _vfxParts.Length; i++)
        {
            _vfxParts[i].transform.localScale *= _VFXSizeMultiplier;
            _vfxParts[i].Play();
        }
    }

    private void DoDamageToAllInArea()
    {
        Collider[] affecteds = Physics.OverlapSphere(_transform.position, _explosionRadius, _enemyLayer);

        if (affecteds.Length > 0)
        {
            for (int i = 0; i < affecteds.Length; i++)
            {
                if (affecteds[i].TryGetComponent(out IDamageable damageable) == true)
                {
                    damageable.TakeDamaget(_damage);
                }
            }
        }
    }

    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(_destroyTimeAfterExplosion);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_transform == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
}
