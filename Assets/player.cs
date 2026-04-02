using UnityEngine;

public class player : MonoBehaviour
{
public KeyCode moveLeft = KeyCode.A;
public KeyCode moveRight = KeyCode.D;
public KeyCode moveUp = KeyCode.W;
public KeyCode moveDown = KeyCode.S;
public KeyCode shootKey = KeyCode.Space;

public float velocidade = 10f;
// Prefab do raio
public GameObject raioPrefab;

// Ponto de onde o raio sai
public Transform pontoDisparo;

// Paredes
public Transform leftWall;
public Transform rightWall;
public Transform topWall;
public Transform bottomWall;

void Update()
{
float movimento = 0f;
float movimentoVertical = 0f;
if (Input.GetKey(moveLeft))
movimento = -1f;

if (Input.GetKey(moveRight))
movimento = 1f;
if(Input.GetKey(moveUp))
movimentoVertical = 1f;
if(Input.GetKey(moveDown))
movimentoVertical = -1f;
// Move o jogador
Vector3 deslocamento = Vector3.right * movimento * velocidade * Time.deltaTime;
Vector3 deslocamentoVert = Vector3.up * movimentoVertical * velocidade * Time.deltaTime;

transform.Translate(deslocamento);
transform.Translate(deslocamentoVert);

// Limites das paredes
if (leftWall != null && rightWall != null && topWall != null && bottomWall != null)
{
Vector3 posicaoAtual = transform.position;
posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, leftWall.position.x, rightWall.position.x);
posicaoAtual.y = Mathf.Clamp(posicaoAtual.y, bottomWall.position.y, topWall.position.y);
transform.position = posicaoAtual;
}

// Atirar
if (Input.GetKeyDown(shootKey))
{
Atirar();
}
}

void Atirar()
{
if (raioPrefab != null && pontoDisparo != null)
{
Instantiate(raioPrefab, pontoDisparo.position, Quaternion.identity);
}
}
}

