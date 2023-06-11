using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Text canText;
    public Text AltinText;
    public Image SiyahArkaPlan;
    int can = 100;


    SpriteRenderer spriteRendere;

    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int altinSayaci = 0;


    Rigidbody2D fizik;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float siyahArkaPlanSayaci = 0;


    bool birKereZipla = true;

    float anaMenuyeDonZaman = 0;

    GameObject kamera;

    void Start()
    {
        Time.timeScale = 1;
        SiyahArkaPlan.gameObject.SetActive(false);
        spriteRendere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }



        kameraIlkPos = kamera.transform.position - transform.position;
        canText.text = "CAN  " + can;
        AltinText.text = "PUAN: " + altinSayaci;
    }

    void Update()
    {

        if (CrossPlatformInputManager.GetButtonDown("Jump"))

        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZipla = false;
            }

        }
    
    }

    void FixedUpdate()
    {
        karakterHareket();
        Animasyon();

        if (can <= 0)
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            siyahArkaPlanSayaci += 0.03f;
            SiyahArkaPlan.gameObject.SetActive(true);
            SiyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman > 1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }
    }

    void LateUpdate()
    {
        kameraKontrol();

    }



    void karakterHareket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        birKereZipla = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kursun")
        {
            can-=10;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "dusman")
        {
            can -= 10;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        
        if (collision.gameObject.tag == "canver")
        {
            if (can!=100)
            {
                can += 10;
                canText.text = "CAN " + can;
                collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.GetComponent<canver>().enabled = true;
                Destroy(collision.gameObject, 3);
            }
            Destroy(collision.gameObject, 3);



        }
        if (collision.gameObject.tag == "altin")
        {
            altinSayaci++;
            AltinText.text = "PUAN: " + altinSayaci;
            Debug.Log(altinSayaci++);
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.tag == "su")
        {
            can = 0;

        }
        if (collision.gameObject.tag == "bos")
        {
            can = 0;

        }

    }
        void kameraKontrol()
        {
            kameraSonPos = kameraIlkPos + transform.position;
            kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.08f);
        }

        void Animasyon()
        {
            if (birKereZipla)
            {
                if (horizontal == 0)
                {
                    beklemeAnimZaman += Time.deltaTime;
                    if (beklemeAnimZaman > 0.05f)
                    {
                        spriteRendere.sprite = beklemeAnim[beklemeAnimSayac++];
                        if (beklemeAnimSayac == beklemeAnim.Length)
                        {
                            beklemeAnimSayac = 0;
                        }
                        beklemeAnimZaman = 0;
                    }

                }
                else if (horizontal > 0)
                {
                    yurumeAnimZaman += Time.deltaTime;
                    if (yurumeAnimZaman > 0.01f)
                    {
                        spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];
                        if (yurumeAnimSayac == yurumeAnim.Length)
                        {
                            yurumeAnimSayac = 0;
                        }
                        yurumeAnimZaman = 0;
                    }
                    transform.localScale = new Vector3(1, 1, 1);

                }
                else if (horizontal < 0)
                {
                    yurumeAnimZaman += Time.deltaTime;
                    if (yurumeAnimZaman > 0.01f)
                    {
                        spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];
                        if (yurumeAnimSayac == yurumeAnim.Length)
                        {
                            yurumeAnimSayac = 0;
                        }
                        yurumeAnimZaman = 0;
                    }
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                if (fizik.velocity.y > 0)
                {
                    spriteRendere.sprite = ziplamaAnim[0];
                }
                else
                {
                    spriteRendere.sprite = ziplamaAnim[1];
                }
                if (horizontal > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (horizontal < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }

        }

   }

