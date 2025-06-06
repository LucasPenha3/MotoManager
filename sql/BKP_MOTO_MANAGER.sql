PGDMP  '    1                }            MotoManager    17.4    17.4 &    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16388    MotoManager    DATABASE     s   CREATE DATABASE "MotoManager" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'pt-BR';
    DROP DATABASE "MotoManager";
                     postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                     pg_database_owner    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                        pg_database_owner    false    4            �            1259    16549 
   entregador    TABLE     ;  CREATE TABLE public.entregador (
    identificador integer NOT NULL,
    nome character varying(255) NOT NULL,
    cnpj character varying(14) NOT NULL,
    datanascimento date NOT NULL,
    numerocnh character varying(20) NOT NULL,
    tipocnh character varying(3) NOT NULL,
    imagemcnh character varying(255)
);
    DROP TABLE public.entregador;
       public         heap r       postgres    false    4            �            1259    16548    entregador_identificador_seq    SEQUENCE     �   CREATE SEQUENCE public.entregador_identificador_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.entregador_identificador_seq;
       public               postgres    false    229    4            �           0    0    entregador_identificador_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public.entregador_identificador_seq OWNED BY public.entregador.identificador;
          public               postgres    false    228            �            1259    16585    locacoes    TABLE     f  CREATE TABLE public.locacoes (
    id integer NOT NULL,
    identregador integer NOT NULL,
    idmoto character varying(100) NOT NULL,
    datainicio timestamp with time zone NOT NULL,
    datatermino timestamp with time zone,
    dataprevisaotermino timestamp with time zone NOT NULL,
    plano integer NOT NULL,
    dataentrega timestamp with time zone
);
    DROP TABLE public.locacoes;
       public         heap r       postgres    false    4            �            1259    16584    locacoes_id_seq    SEQUENCE     �   CREATE SEQUENCE public.locacoes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.locacoes_id_seq;
       public               postgres    false    4    231            �           0    0    locacoes_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.locacoes_id_seq OWNED BY public.locacoes.id;
          public               postgres    false    230            �            1259    16528    motos    TABLE     �   CREATE TABLE public.motos (
    identificador character varying(100) NOT NULL,
    ano integer NOT NULL,
    modelo character varying(255) NOT NULL,
    placa character varying(10) NOT NULL
);
    DROP TABLE public.motos;
       public         heap r       postgres    false    4            �           0    0    TABLE motos    ACL     +   GRANT ALL ON TABLE public.motos TO PUBLIC;
          public               postgres    false    225            �            1259    16542    planos    TABLE     �   CREATE TABLE public.planos (
    id integer NOT NULL,
    dias integer NOT NULL,
    preco numeric(5,2) NOT NULL,
    multapercentualadiantamento numeric(5,2) NOT NULL,
    adicionaldiaria numeric(5,2) NOT NULL
);
    DROP TABLE public.planos;
       public         heap r       postgres    false    4            �            1259    16541    planos_id_seq    SEQUENCE     �   CREATE SEQUENCE public.planos_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.planos_id_seq;
       public               postgres    false    4    227            �           0    0    planos_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.planos_id_seq OWNED BY public.planos.id;
          public               postgres    false    226            @           2604    16552    entregador identificador    DEFAULT     �   ALTER TABLE ONLY public.entregador ALTER COLUMN identificador SET DEFAULT nextval('public.entregador_identificador_seq'::regclass);
 G   ALTER TABLE public.entregador ALTER COLUMN identificador DROP DEFAULT;
       public               postgres    false    229    228    229            A           2604    16588    locacoes id    DEFAULT     j   ALTER TABLE ONLY public.locacoes ALTER COLUMN id SET DEFAULT nextval('public.locacoes_id_seq'::regclass);
 :   ALTER TABLE public.locacoes ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    230    231    231            ?           2604    16545 	   planos id    DEFAULT     f   ALTER TABLE ONLY public.planos ALTER COLUMN id SET DEFAULT nextval('public.planos_id_seq'::regclass);
 8   ALTER TABLE public.planos ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    226    227    227            �          0    16549 
   entregador 
   TABLE DATA           n   COPY public.entregador (identificador, nome, cnpj, datanascimento, numerocnh, tipocnh, imagemcnh) FROM stdin;
    public               postgres    false    229   �,       �          0    16585    locacoes 
   TABLE DATA           ~   COPY public.locacoes (id, identregador, idmoto, datainicio, datatermino, dataprevisaotermino, plano, dataentrega) FROM stdin;
    public               postgres    false    231   ]-       �          0    16528    motos 
   TABLE DATA           B   COPY public.motos (identificador, ano, modelo, placa) FROM stdin;
    public               postgres    false    225   �-       �          0    16542    planos 
   TABLE DATA           _   COPY public.planos (id, dias, preco, multapercentualadiantamento, adicionaldiaria) FROM stdin;
    public               postgres    false    227   .       �           0    0    entregador_identificador_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.entregador_identificador_seq', 6, true);
          public               postgres    false    228            �           0    0    locacoes_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.locacoes_id_seq', 2, true);
          public               postgres    false    230            �           0    0    planos_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.planos_id_seq', 1, false);
          public               postgres    false    226            J           2606    16559    entregador entregador_cnpj_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.entregador
    ADD CONSTRAINT entregador_cnpj_key UNIQUE (cnpj);
 H   ALTER TABLE ONLY public.entregador DROP CONSTRAINT entregador_cnpj_key;
       public                 postgres    false    229            L           2606    16561 #   entregador entregador_numerocnh_key 
   CONSTRAINT     c   ALTER TABLE ONLY public.entregador
    ADD CONSTRAINT entregador_numerocnh_key UNIQUE (numerocnh);
 M   ALTER TABLE ONLY public.entregador DROP CONSTRAINT entregador_numerocnh_key;
       public                 postgres    false    229            N           2606    16557    entregador entregador_pkey 
   CONSTRAINT     c   ALTER TABLE ONLY public.entregador
    ADD CONSTRAINT entregador_pkey PRIMARY KEY (identificador);
 D   ALTER TABLE ONLY public.entregador DROP CONSTRAINT entregador_pkey;
       public                 postgres    false    229            B           2606    16608 $   entregador entregador_tipocnh_check2    CHECK CONSTRAINT     �   ALTER TABLE public.entregador
    ADD CONSTRAINT entregador_tipocnh_check2 CHECK (((tipocnh)::text = ANY (ARRAY[('0'::character varying)::text, ('1'::character varying)::text, ('2'::character varying)::text]))) NOT VALID;
 I   ALTER TABLE public.entregador DROP CONSTRAINT entregador_tipocnh_check2;
       public               postgres    false    229    229            P           2606    16590    locacoes locacoes_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.locacoes
    ADD CONSTRAINT locacoes_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.locacoes DROP CONSTRAINT locacoes_pkey;
       public                 postgres    false    231            D           2606    16532    motos motos_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.motos
    ADD CONSTRAINT motos_pkey PRIMARY KEY (identificador);
 :   ALTER TABLE ONLY public.motos DROP CONSTRAINT motos_pkey;
       public                 postgres    false    225            F           2606    16534    motos motos_placa_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.motos
    ADD CONSTRAINT motos_placa_key UNIQUE (placa);
 ?   ALTER TABLE ONLY public.motos DROP CONSTRAINT motos_placa_key;
       public                 postgres    false    225            H           2606    16547    planos planos_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.planos
    ADD CONSTRAINT planos_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.planos DROP CONSTRAINT planos_pkey;
       public                 postgres    false    227            Q           2606    16591 #   locacoes locacoes_identregador_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.locacoes
    ADD CONSTRAINT locacoes_identregador_fkey FOREIGN KEY (identregador) REFERENCES public.entregador(identificador);
 M   ALTER TABLE ONLY public.locacoes DROP CONSTRAINT locacoes_identregador_fkey;
       public               postgres    false    231    4686    229            R           2606    16596    locacoes locacoes_idmoto_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.locacoes
    ADD CONSTRAINT locacoes_idmoto_fkey FOREIGN KEY (idmoto) REFERENCES public.motos(identificador);
 G   ALTER TABLE ONLY public.locacoes DROP CONSTRAINT locacoes_idmoto_fkey;
       public               postgres    false    225    4676    231            S           2606    16601    locacoes locacoes_plano_fkey    FK CONSTRAINT     z   ALTER TABLE ONLY public.locacoes
    ADD CONSTRAINT locacoes_plano_fkey FOREIGN KEY (plano) REFERENCES public.planos(id);
 F   ALTER TABLE ONLY public.locacoes DROP CONSTRAINT locacoes_plano_fkey;
       public               postgres    false    231    4680    227            �   ~   x�3�,.)��K�QFF��&�F�0�?.SN����bNC Tmh Um@�V11��%��11)��`Ba2��kh�kj�e1V! 5/#�p ��P��P��a�!�-t,t�u�M�b���� �c6z      �   g   x�u��	1D�o�
7`���U-鿎ā����G�d�B�h�W�%W׸��G[��o���*��dhl+�)�s�"�'��9���G��s���d�t�`�ї�RޖG+�      �   /   x�342�4202�LN7�,�I424�24�9;���b���� �u	_      �   @   x�]��  �w�@��0�?�b|?mri]�*x32�� g��g
2���㽋�i��t5�#��     