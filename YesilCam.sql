--
-- PostgreSQL database dump
--

-- Dumped from database version 15.5
-- Dumped by pg_dump version 15.5

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: FilmTurleri; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."FilmTurleri" (
    tur_id integer NOT NULL,
    tur_ismi text
);


ALTER TABLE public."FilmTurleri" OWNER TO postgres;

--
-- Name: FilmTurleri_tur_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."FilmTurleri_tur_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."FilmTurleri_tur_id_seq" OWNER TO postgres;

--
-- Name: FilmTurleri_tur_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."FilmTurleri_tur_id_seq" OWNED BY public."FilmTurleri".tur_id;


--
-- Name: Filmler; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Filmler" (
    film_id integer NOT NULL,
    isim text,
    rating numeric,
    butce integer,
    yapim_yili date,
    gise_sayisi integer,
    afis_patch text,
    kisi_id integer,
    tur_id integer
);


ALTER TABLE public."Filmler" OWNER TO postgres;

--
-- Name: Filmler_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Filmler_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Filmler_id_seq" OWNER TO postgres;

--
-- Name: Filmler_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Filmler_id_seq" OWNED BY public."Filmler".film_id;


--
-- Name: Oduller; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Oduller" (
    film_id integer NOT NULL,
    kisi_id integer NOT NULL,
    isim text
);


ALTER TABLE public."Oduller" OWNER TO postgres;

--
-- Name: Oyuncu_Film; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Oyuncu_Film" (
    kisi_id integer NOT NULL,
    film_id integer NOT NULL
);


ALTER TABLE public."Oyuncu_Film" OWNER TO postgres;

--
-- Name: Oyuncu_ve_Yonetmen; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Oyuncu_ve_Yonetmen" (
    kisi_id integer NOT NULL,
    sahne_isim text,
    sahne_soyisim text,
    gercek_isim text,
    gercek_soyisim text,
    dogum_tarihi date,
    cinsiyet text,
    kisi_tipi text
);


ALTER TABLE public."Oyuncu_ve_Yonetmen" OWNER TO postgres;

--
-- Name: Oyuncular_oyuncu_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Oyuncular_oyuncu_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Oyuncular_oyuncu_id_seq" OWNER TO postgres;

--
-- Name: Oyuncular_oyuncu_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Oyuncular_oyuncu_id_seq" OWNED BY public."Oyuncu_ve_Yonetmen".kisi_id;


--
-- Name: FilmTurleri tur_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FilmTurleri" ALTER COLUMN tur_id SET DEFAULT nextval('public."FilmTurleri_tur_id_seq"'::regclass);


--
-- Name: Filmler film_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Filmler" ALTER COLUMN film_id SET DEFAULT nextval('public."Filmler_id_seq"'::regclass);


--
-- Name: Oyuncu_ve_Yonetmen kisi_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_ve_Yonetmen" ALTER COLUMN kisi_id SET DEFAULT nextval('public."Oyuncular_oyuncu_id_seq"'::regclass);


--
-- Data for Name: FilmTurleri; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."FilmTurleri" (tur_id, tur_ismi) FROM stdin;
1	Komedi
2	Dram
3	Tarihi
4	Aksiyon
5	Romantik
6	Bilim Kurgu
7	Gerilim
8	Fantastik
9	Suç
10	Belgesel
11	Animasyon
\.


--
-- Data for Name: Filmler; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Filmler" (film_id, isim, rating, butce, yapim_yili, gise_sayisi, afis_patch, kisi_id, tur_id) FROM stdin;
17	Sahte Kabadayi	9.7	950000	1976-01-01	1550000	\\afisler\\sahtekabadayi.jpg	22	1
6	Copculer Krali	8.9	678000	1978-02-01	1200000	\\afisler\\copculerkrali.jpg	22	1
1	Neseli Gunler	8.2	500000	1978-04-01	1540000	\\afisler\\neseligunler.jpg	21	1
2	Kanli Nigar	9.7	542000	1978-11-01	700000	\\afisler\\kanlinigar.jpg	21	1
3	Cicek Abbas	8.6	600000	1982-09-01	350000	\\afisler\\cicekabbas.jpg	21	1
4	Bizim Aile	8.2	80000	1976-02-01	800000	\\afisler\\bizimaile.jpg	24	\N
7	Gulen Gozler	9.1	985500	1977-03-01	678000	\\afisler\\gulengozler.jpg	27	\N
8	Davaro	7.7	1500000	1981-01-01	987000	\\afisler\\davaro.jpg	28	\N
9	Gulsah	6.6	620000	1975-01-01	750000	\\afisler\\gulsah.jpg	29	\N
10	Sut Kardesler	9.2	550000	1976-01-01	840000	\\afisler\\sutkardesler.jpg	30	\N
5	Mavi Boncukk	7.8	760000	1975-01-01	550000	\\afisler\\maviboncuk.jpg	26	1
93	Selvi Boylum Al Yazmalım	8.2	1000000	1978-01-01	500000	\\afisler\\selviboylumalyazmalim.jpg	21	1
94	Dünyayı Kurtaran Adam	6.5	500000	1982-01-01	200000	\\afisler\\dunyayikurtaranadam.jpg	22	1
95	Hababam Sınıfı	7.9	800000	1975-01-01	700000	\\afisler\\hababamsinifi.jpg	23	1
97	Şabanoğlu Şaban	6.3	700000	1985-01-01	450000	\\afisler\\sabanoglusaban.jpg	26	1
98	Mavi Mavi	7.1	950000	1985-01-01	500000	\\afisler\\mavimavi.jpg	27	1
100	Kibar Feyzo	7.4	850000	1978-01-01	560000	\\afisler\\kibarfeyzo.jpg	30	1
101	Malkoçoğlu	6.9	600000	1971-01-01	350000	\\afisler\\malkocoglu.jpg	21	1
102	Battal Gazinin Oğlu	7.2	700000	1972-01-01	420000	\\afisler\\battalgazi.jpg	22	1
106	Hanzo	6.4	950000	1985-01-01	480000	\\afisler\\hanzo.jpg	27	1
107	Zübük	7.3	800000	1980-01-01	550000	\\afisler\\zubuk.jpg	28	1
108	Bekçiler Kralı	6.8	700000	1976-01-01	420000	\\afisler\\bekcilerkrali.jpg	29	1
109	Maden	7.1	750000	1978-01-01	500000	\\afisler\\maden.jpg	30	1
110	Kara Murat	6.6	600000	1977-01-01	350000	\\afisler\\karamurat.jpg	30	1
111	Doktor Civanım	7.2	700000	1975-01-01	420000	\\afisler\\doktorcivanim.jpg	30	1
112	Bir Yudum Sevgi	7.9	900000	1984-01-01	520000	\\afisler\\biryudumsevgi.jpg	23	1
113	Battal Gazi Geliyor	6.7	750000	1973-01-01	480000	\\afisler\\battalgazigeliyor.jpg	24	1
114	Tarkan: Gümüş Eyer	7.5	800000	1975-01-01	550000	\\afisler\\tarkangumuseyer.jpg	25	1
115	Dünyayı Kurtaran Adamın Oğlu	6.2	2000000	2006-01-01	1000000	\\afisler\\dunyayikurtaran_adaminoglu.jpg	26	1
116	Beyaz Melek	8.1	2500000	2007-01-01	1200000	\\afisler\\beyazmelek.jpg	28	1
117	Uçurtmayı Vurmasınlar	7.8	1200000	1989-01-01	700000	\\afisler\\ucurtmayivurmasinlar.jpg	29	1
\.


--
-- Data for Name: Oduller; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Oduller" (film_id, kisi_id, isim) FROM stdin;
1	21	Altın Kelebek
2	21	Altın Kelebek
5	22	Altın Portakal
6	23	Altın Portakal
\.


--
-- Data for Name: Oyuncu_Film; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Oyuncu_Film" (kisi_id, film_id) FROM stdin;
2	1
2	2
4	3
8	5
15	5
2	17
4	17
6	17
8	17
2	6
4	6
6	6
8	6
\.


--
-- Data for Name: Oyuncu_ve_Yonetmen; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Oyuncu_ve_Yonetmen" (kisi_id, sahne_isim, sahne_soyisim, gercek_isim, gercek_soyisim, dogum_tarihi, cinsiyet, kisi_tipi) FROM stdin;
2	Kemal	Sunal	Kemal	Sunal	1944-11-11	Erkek	Oyuncu
4	Tarik	Akan	Tarik	Akan	1949-07-02	Erkek	Oyuncu
6	Cuneyt	Arkin	Cuneyt	Arkin	1937-09-04	Erkek	Oyuncu
8	Ediz	Hun	Ediz	Hun	1940-01-27	Erkek	Oyuncu
10	Sadri	Alisik	Sadri	Alisik	1925-11-05	Erkek	Oyuncu
12	Kadir	Inanir	Kadir	Inanir	1949-04-15	Erkek	Oyuncu
13	Behcet	Nacar	Behcet	Nacar	1934-07-15	Erkek	Oyuncu
15	Sener	Sen	Sener	Sen	1941-12-26	Erkek	Oyuncu
17	Kuzey	Vargin	Kuzey	Vargin	1945-11-01	Erkek	Oyuncu
19	Erol	Gunaydin	Erol	Gunaydin	1933-08-19	Erkek	Oyuncu
3	Hulya	Kocyigit	Hulya	Kocyigit	1947-12-12	Kadin	Oyuncu
5	Mujde	Ar	Mujde	Ar	1954-06-21	Kadin	Oyuncu
7	Fatma	Girik	Fatma	Girik	1942-12-12	Kadin	Oyuncu
9	Ajda	Pekkan	Ajda	Pekkan	1946-02-14	Kadin	Oyuncu
11	Necla	Nazir	Necla	Nazir	1930-01-01	Kadin	Oyuncu
14	Perihan	Savas	Perihan	Savas	1949-07-01	Kadin	Oyuncu
16	Serpil	Cakmakli	Serpil	Cakmakli	1948-03-29	Kadin	Oyuncu
18	Gulistan	Guven	Gulistan	Guven	1944-04-01	Kadin	Oyuncu
20	Nur	Surer	Nur	Surer	1944-12-22	Kadin	Oyuncu
22	Atıf	Yılmaz	Atıf	Yılmaz	1925-12-25	Erkek	Yonetmen
23	Halit	Refiğ	Halit	Refiğ	1924-11-04	Erkek	Yonetmen
24	Metin	Erksan	Metin	Erksan	1929-12-02	Erkek	Yonetmen
25	Şerif	Gören	Şerif	Gören	1944-12-04	Erkek	Yonetmen
26	Memduh	Ün	Memduh	Ün	1920-08-17	Erkek	Yonetmen
27	Duygu	Sağıroğlu	Duygu	Sağıroğlu	1949-01-01	Kadın	Yonetmen
28	Ertem	Eğilmez	Ertem	Eğilmez	1929-06-04	Erkek	Yonetmen
29	Hulki	Saner	Hulki	Saner	1927-12-20	Erkek	Yonetmen
30	Sinan	Çetin	Sinan	Çetin	1955-03-01	Erkek	Yonetmen
21	Ergin	Orbey	Ergin	Orbey	1937-04-01	Erkek	Yonetmen
31	Cihan	Ünal	Cihan	Ünal	1950-03-15	Erkek	Oyuncu
32	Adile	Naşit	Adile	Naşit	1930-06-17	Kadın	Oyuncu
33	Türkan	Şoray	Türkan	Şoray	1945-06-28	Kadın	Oyuncu
34	Münir	Özkul	Münir	Özkul	1925-08-15	Erkek	Oyuncu
35	Halit	Akmansü	Halit	Akmansü	1930-01-01	Erkek	Oyuncu
36	Ahmet	Mekin	Ahmet	Mekin	1932-05-16	Erkek	Oyuncu
37	Erol	Taş	Erol	Taş	1928-02-04	Erkek	Oyuncu
38	Şehime	Erton	Şehime	Erton	1943-08-14	Kadın	Oyuncu
39	Bülent	Kayabaş	Bülent	Kayabaş	1939-11-12	Erkek	Oyuncu
40	Nur	Sürer	Nur	Sürer	1954-04-13	Kadın	Oyuncu
41	Hulusi	Kentmen	Hulusi	Kentmen	1927-09-11	Erkek	Oyuncu
42	Kamran	Usluer	Kamran	Usluer	1936-01-01	Erkek	Oyuncu
43	Meral	Zeren	Meral	Zeren	1943-03-17	Kadın	Oyuncu
44	Mehmet Ali	Erdem	Mehmet Ali	Erdem	1928-05-22	Erkek	Oyuncu
45	Nevra	Serezli	Nevra	Serezli	1944-12-14	Kadın	Oyuncu
46	İlyas	Salman	İlyas	Salman	1938-01-14	Erkek	Oyuncu
47	Nilüfer	Açıkalın	Nilüfer	Açıkalın	1954-01-31	Kadın	Oyuncu
48	Birsen	Duru	Birsen	Duru	1945-12-19	Kadın	Oyuncu
49	Kerem	Alışık	Kerem	Alışık	1948-06-05	Erkek	Oyuncu
50	Perihan	Savaş	Perihan	Savaş	1945-03-09	Kadın	Oyuncu
51	Şafak	Sezer	Şafak	Sezer	1953-04-30	Erkek	Oyuncu
\.


--
-- Name: FilmTurleri_tur_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."FilmTurleri_tur_id_seq"', 11, true);


--
-- Name: Filmler_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Filmler_id_seq"', 118, true);


--
-- Name: Oyuncular_oyuncu_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Oyuncular_oyuncu_id_seq"', 51, true);


--
-- Name: FilmTurleri FilmTurleri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FilmTurleri"
    ADD CONSTRAINT "FilmTurleri_pkey" PRIMARY KEY (tur_id);


--
-- Name: Filmler Filmler_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Filmler"
    ADD CONSTRAINT "Filmler_pkey" PRIMARY KEY (film_id);


--
-- Name: Oduller Oduller_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oduller"
    ADD CONSTRAINT "Oduller_pkey" PRIMARY KEY (film_id, kisi_id);


--
-- Name: Oyuncu_Film Oyuncu_Film_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_Film"
    ADD CONSTRAINT "Oyuncu_Film_pkey" PRIMARY KEY (kisi_id, film_id);


--
-- Name: Oyuncu_ve_Yonetmen Oyuncular_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_ve_Yonetmen"
    ADD CONSTRAINT "Oyuncular_pkey" PRIMARY KEY (kisi_id);


--
-- Name: Oduller Oduller_film_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oduller"
    ADD CONSTRAINT "Oduller_film_id_fkey" FOREIGN KEY (film_id) REFERENCES public."Filmler"(film_id);


--
-- Name: Oduller Oduller_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oduller"
    ADD CONSTRAINT "Oduller_kisi_id_fkey" FOREIGN KEY (kisi_id) REFERENCES public."Oyuncu_ve_Yonetmen"(kisi_id);


--
-- Name: Oyuncu_Film Oyuncu_Film_film_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_Film"
    ADD CONSTRAINT "Oyuncu_Film_film_id_fkey" FOREIGN KEY (film_id) REFERENCES public."Filmler"(film_id);


--
-- Name: Oyuncu_Film Oyuncu_Film_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_Film"
    ADD CONSTRAINT "Oyuncu_Film_kisi_id_fkey" FOREIGN KEY (kisi_id) REFERENCES public."Oyuncu_ve_Yonetmen"(kisi_id);


--
-- Name: Oyuncu_Film film_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Oyuncu_Film"
    ADD CONSTRAINT film_id_fkey FOREIGN KEY (film_id) REFERENCES public."Filmler"(film_id) ON DELETE CASCADE;


--
-- Name: Filmler fk_filmler_turler; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Filmler"
    ADD CONSTRAINT fk_filmler_turler FOREIGN KEY (tur_id) REFERENCES public."FilmTurleri"(tur_id);


--
-- Name: Filmler fk_filmler_yonetmen; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Filmler"
    ADD CONSTRAINT fk_filmler_yonetmen FOREIGN KEY (kisi_id) REFERENCES public."Oyuncu_ve_Yonetmen"(kisi_id);


--
-- PostgreSQL database dump complete
--

