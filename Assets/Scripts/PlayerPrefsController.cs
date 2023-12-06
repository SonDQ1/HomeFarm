using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    public static PlayerPrefsController instance;
    private void Awake()
    {
        instance = this;
    }

    public void setFirstApp()
    {
        PlayerPrefs.SetInt("firstApp0", 1);
    }
    public bool getFirstApp()
    {
        bool firt = true;
        if (PlayerPrefs.GetInt("firstApp0") == 1)
            firt = false;
        return firt;
    }

    //ô đất trồng
    public void setSlOdat(string namelodat, int sl)
    {
        PlayerPrefs.SetInt(namelodat + "slodat", sl);
    }
    public void adSlOdat(string namelodat, int sl)
    {
        PlayerPrefs.SetInt(namelodat + "slodat", getSlodat(namelodat) + sl);
    }
    public int getSlodat(string namelodat)
    {
        return PlayerPrefs.GetInt(namelodat + "slodat");
    }

    public void setSlOdatkhoa(string namelodat, int sl)
    {
        PlayerPrefs.SetInt(namelodat + "slodatkhoa", sl);
    }
    public void adSlOdatkhoa(string namelodat, int sl)
    {
        PlayerPrefs.SetInt(namelodat + "slodatkhoa", getSlodatkhoa(namelodat) + sl);
    }
    public int getSlodatkhoa(string namelodat)
    {
        return PlayerPrefs.GetInt(namelodat + "slodatkhoa");
    }

    //cây trồng
    public void setTimeStartCay(string loodat, int idOdat, int t)
    {
        PlayerPrefs.SetInt("timestartCay" + loodat + idOdat, t);
    }
    public void saveCayOdat(string loodat, int idOdat, int idCay)
    {
        PlayerPrefs.SetInt("odat" + loodat + idOdat, 1);
        PlayerPrefs.SetInt("odat" + loodat + idOdat + "cay", idCay);
        PlayerPrefs.SetInt("timestartCay" + loodat + idOdat, Until.timeNow());
    }
    public void removeCayOdat(string loodat, int idOdat)
    {
        PlayerPrefs.SetInt("odat" + loodat + idOdat, 0);
        PlayerPrefs.SetInt("timestart" + loodat + idOdat, 0);
    }
    public int getTimeStarCay(string loodat, int idOdat)
    {
        return PlayerPrefs.GetInt("timestartCay" + loodat + idOdat);
    }
    public int getIdcay(string loodat, int idOdat)
    {
        return PlayerPrefs.GetInt("odat" + loodat + idOdat + "cay");
    }
    public bool checkCayTrenodat(string loodat, int idOdat)
    {
        if (PlayerPrefs.GetInt("odat" + loodat + idOdat) > 0) return true;
        return false;
    }
    //cây lâu năm
    public void setTuoinuocCayll(string loodat, int idOdat, int value)
    {
        PlayerPrefs.SetInt("tuoinuoc" + loodat + idOdat, value);
    }
    public bool getTuoinuocCayll(string loodat, int idOdat)
    {
        if (PlayerPrefs.GetInt("tuoinuoc" + loodat + idOdat) > 0) return true;
        return false;
    }
    public void setCayllLon(string loodat, int idOdat, int value)
    {
        PlayerPrefs.SetInt("caylllon" + loodat + idOdat, value);
    }
    public bool getCayllLon(string loodat, int idOdat)
    {
        if (PlayerPrefs.GetInt("caylllon" + loodat + idOdat) > 0)
            return true;
        return false;
    }
    public void setTimeStartHeo(string loodat, int idOdat, int value)
    {
        PlayerPrefs.SetInt("timecayllheo" + loodat + idOdat, value);
    }
    public int getTimeStartHeo(string loodat, int idOdat)
    {
        return PlayerPrefs.GetInt("timecayllheo" + loodat + idOdat);
    }
    public void setCayheo(string loodat, int idOdat, int value)
    {
        PlayerPrefs.SetInt("cayheo" + loodat + idOdat, value);
    }
    public bool getCayheo(string loodat, int idOdat)
    {
        if (PlayerPrefs.GetInt("cayheo" + loodat + idOdat) > 0) return true;
        return false;
    }
    //kho
    bool runcheck = false;
    public void addKho(int idvp, int sl)
    {
        PlayerPrefs.SetInt("vpkho" + idvp, sl + getVpKho(idvp));
        if (idvp < GameManagerControll.instance.idCua)
            addExp(1);
        addVpThanhtich(idvp, sl);
        if (!runcheck)
        {
            runcheck = true;
            StartCoroutine(delayCheckOrder());
        }
    }
    public void removeVpKho(int idvp, int sl)
    {
        if (getVpKho(idvp) - sl >= 0)
            PlayerPrefs.SetInt("vpkho" + idvp, getVpKho(idvp) - sl);
        else PlayerPrefs.SetInt("vpkho" + idvp, 0);
        if (!runcheck)
        {
            runcheck = true;
            StartCoroutine(delayCheckOrder());
        }
    }
    public int getVpKho(int idvp)
    {
        return PlayerPrefs.GetInt("vpkho" + idvp);
    }
    //delay check order và load thành tích: khi thu hoạch sẽ addkho check liên tục
    IEnumerator delayCheckOrder()
    {
        yield return new WaitForSeconds(5f);
        OrderController.instance.checkOrder();
        GameManagerControll.instance.checkthanhtich();
        runcheck = false;
    }


    //level
    public void setLevel(int lv)
    {
        PlayerPrefs.SetInt("level", lv);
    }
    public void levelUp()
    {
        if (getLevel() >= Until.maxLevel)
        {
            setLevel(Until.maxLevel);
        }
        else
            PlayerPrefs.SetInt("level", getLevel() + 1);
    }

    public int getLevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    //coin
    public void addCoin(int c)
    {
        PlayerPrefs.SetInt("coin", getCoin() + c);
        GameManagerControll.instance.loadUIMain();
    }
    public void exceptCoin(int c)
    {
        if (getCoin() - c >= 0)
            PlayerPrefs.SetInt("coin", getCoin() - c);
        else
            PlayerPrefs.SetInt("coin", 0);
        GameManagerControll.instance.loadUIMain();
    }
    public int getCoin()
    {
        return PlayerPrefs.GetInt("coin");
    }
    //gem
    public void addGem(int g)
    {
        PlayerPrefs.SetInt("gem", getGem() + g);
        GameManagerControll.instance.loadUIMain();
    }
    public void exceptGem(int g)
    {
        if (getGem() - g >= 0)
            PlayerPrefs.SetInt("gem", getGem() - g);
        else
            PlayerPrefs.SetInt("gem", 0);
        GameManagerControll.instance.loadUIMain();
    }
    public int getGem()
    {
        return PlayerPrefs.GetInt("gem");
    }

    //Diamod
    public void addDiamod(int d)
    {
        PlayerPrefs.SetInt("Diamod", getDiamod() + d);
        GameManagerControll.instance.loadUIMain();
    }
    public void exceptDiamod(int d)
    {
        if (getDiamod() - d >= 0)
            PlayerPrefs.SetInt("Diamod", getDiamod() - d);
        else
            PlayerPrefs.SetInt("Diamod", 0);

        GameManagerControll.instance.loadUIMain();
    }
    public int getDiamod()
    {
        return PlayerPrefs.GetInt("Diamod");
    }
    //exp
    public void setExp(int e)
    {
        PlayerPrefs.SetInt("exp", e);
        GameManagerControll.instance.loadUIMain();
    }
    public int getExp()
    {
        return PlayerPrefs.GetInt("exp");
    }
    public void addExp(int e)
    {
        if (getExp() + e >= GameManagerControll.instance.targetExp[getLevel()])
        {
            int expnew = (getExp() + e) - GameManagerControll.instance.targetExp[getLevel()];
            setExp(expnew);
            GameManagerControll.instance.levelUp();
        }
        else
            PlayerPrefs.SetInt("exp", getExp() + e);

        GameManagerControll.instance.loadUIMain();
    }

    //phát triển chuồng - con vật
    public void setTimeStartConvat(int idLuoi, int idConvat, int t)
    {
        PlayerPrefs.SetInt("timeStartConvat" + idLuoi + idConvat, t);
    }
    public void setChoan(int idLuoi, int idConvat, int i)
    {
        PlayerPrefs.SetInt("choan" + idLuoi + idConvat, i);
    }

    public int getTimeStartConvat(int idLuoi, int idConvat)
    {
        return PlayerPrefs.GetInt("timeStartConvat" + idLuoi + idConvat);
    }
    public int getChoan(int idLuoi, int idConvat)
    {
        return PlayerPrefs.GetInt("choan" + idLuoi + idConvat);
    }

    //mỗi nhà máy chỉ được xây 1 lần
    public void setNhamay(int id, int sl)
    {
        PlayerPrefs.SetInt("nhamay" + id, sl);
    }
    public int checkXayNhamay(int id)
    {
        return PlayerPrefs.GetInt("nhamay" + id);
    }

    public void addIdNha(string name)
    {
        PlayerPrefs.SetInt("nha" + name, getIdNha(name) + 1);
    }
    public int getIdNha(string name)
    {
        return PlayerPrefs.GetInt("nha" + name);
    }

    public void saveIdNhamayLuoi(int idluoi, int idnhamay)
    {
        PlayerPrefs.SetInt("nhamayluoi" + idluoi, idnhamay);
    }
    public int getIdNhamayLuoi(int idluoi)
    {
        return PlayerPrefs.GetInt("nhamayluoi" + idluoi);
    }
    //thêm ô sản xuất nhà máy
    public void themOsx(string objtag, int idnhamay, int idO, int value)
    {
        PlayerPrefs.SetInt("themosx" + objtag + idnhamay + idO, value);
    }
    public int getOsx(string objtag, int idnhamay, int idO)
    {
        return PlayerPrefs.GetInt("themosx" + objtag + idnhamay + idO);
    }

    //thêm sp phát triển
    public void setTimeStartSanpham(string objtag, int idnhamay, int time)
    {
        PlayerPrefs.SetInt("timestartsp" + objtag + idnhamay, time);
    }
    public int getTimeStartSP(string objtag, int idnhamay)
    {
        return PlayerPrefs.GetInt("timestartsp" + objtag + idnhamay);
    }
    //thêm sp phát triển
    public void addListVp(string objtag, int idNhamay, int idsp)
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("listMake" + objtag + idNhamay + i) <= 0)
            {
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + i, idsp);
                break;
            }
        }
    }
    public void removeListMake(string objtag, int idNhamay, int count)
    {
        //Debug.Log("removeListMake: " + count);
        //thay đổi lại thứ tự trong mảng
        switch (count)
        {
            case 4:
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 0, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 1));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 1, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 2));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 2, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 3));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 3, 0);
                break;
            case 3:
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 0, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 1));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 1, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 2));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 2, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 3, 0);
                break;
            case 2:
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 0, PlayerPrefs.GetInt("listMake" + objtag + idNhamay + 1));
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 1, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 2, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 3, 0);
                break;
            case 1:
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 0, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 1, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 2, 0);
                PlayerPrefs.SetInt("listMake" + objtag + idNhamay + 3, 0);
                break;
        }
    }

    public List<int> getListMake(string objtag, int idNhamay)
    {
        //Debug.Log("getListMake==================>>>>");
        List<int> list = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("listMake" + objtag + idNhamay + i) > 0)
            {
                list.Add(PlayerPrefs.GetInt("listMake" + objtag + idNhamay + i));
            }
        }
        //Debug.Log("getListMake = " + list.Count);
        return list;
    }

    //check khi thời gian hiện tại lớn hơn thời gian tạo ra các sản phẩm
    public void setCheckTimeStart(int idsp, int value)
    {
        PlayerPrefs.SetInt("checktimestartsp" + idsp, value);
    }
    public int getCheckTimeStart(int idsp)
    {
        return PlayerPrefs.GetInt("checktimestartsp" + idsp);
    }
    public bool checkTime(int idsp)
    {
        if (PlayerPrefs.GetInt("checktimestartsp" + idsp) >= 0)
            return true;
        return false;
    }

    //set thu hoạch
    public bool checkThuhoachSP(string objtag, int idNhamay, int idsp)
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + i) == idsp)
            {
                return true;
            }
        }
        return false;
    }
    public void setThuhoach(string objtag, int idNhamay, int idsp)
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + i) <= 0)
            {
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + i, idsp);
                break;
            }
        }
    }
    public List<int> getListThuhoach(string objtag, int idNhamay)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + i) > 0)
            {
                list.Add(PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + i));
            }
        }
        return list;
    }

    public void newListThuhoach(string objtag, int idNhamay)
    {
        Debug.Log("newListThuhoach==================>>>>");
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + i, 0);
        }
    }
    public void removeItemListThuhoach(string objtag, int idNhamay, int idPos)
    {
        Debug.Log("removeItemListThuhoach==================>>>>");
        switch (idPos)
        {
            case 0:
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 0, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 1));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 1, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 2));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 2, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 3));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 3, 0);
                break;
            case 1:
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 1, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 2));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 2, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 3));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 3, 0);
                break;
            case 2:
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 2, PlayerPrefs.GetInt("thuhoach" + objtag + idNhamay + 3));
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 3, 0);
                break;
            case 3:
                PlayerPrefs.SetInt("thuhoach" + objtag + idNhamay + 3, 0);
                break;
        }
    }
    #region sản xuất nhà máy new - lỗi
    //new 
    //sản xuất nhà máy
    //public void setTimeStartSanpham(int idnhamay, int idoMake, int idsp, int time)
    //{
    //    PlayerPrefs.SetInt("listMakeTimestart" + idnhamay + idoMake + idsp, time);
    //}
    //public int getTimeStartSP(int idnhamay, int idoMake, int idsp)
    //{
    //    return PlayerPrefs.GetInt("listMakeTimestart" + idnhamay + idoMake + idsp);
    //}
    public void addListMake(int idNhamay, int idomake, int idsp, int timestart)
    {
        if (PlayerPrefs.GetInt("listMake" + idNhamay + idomake) <= 0)
        {
            PlayerPrefs.SetInt("listMake" + idNhamay + idomake, idsp);
            PlayerPrefs.SetInt("listMakeTimestart" + idNhamay + idomake + idsp, timestart);
        }
    }
    public List<ItemMake> getListItemMake(int idNhamay)
    {
        List<ItemMake> itemMakes = new List<ItemMake>();
        for (int i = 0; i < 4; i++)
        {
            int idsp = PlayerPrefs.GetInt("listMake" + idNhamay + i);
            int timestart = PlayerPrefs.GetInt("listMakeTimestart" + idNhamay + i + idsp);
            if (idsp > 0)
            {
                ItemMake itemMake = new ItemMake(idsp, timestart);
                itemMakes.Add(itemMake);
            }
        }
        return itemMakes;
    }
    public void removeItemListMake(int idNhamay, int idsp)
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("listMake" + idNhamay + i) == idsp)
            {
                PlayerPrefs.SetInt("listMake" + idNhamay + i, 0);
                //thay đổi lại thứ tự trong mảng
                switch (i)
                {
                    case 0:
                        PlayerPrefs.SetInt("listMake" + idNhamay + 0, PlayerPrefs.GetInt("listMake" + idNhamay + 1));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 1, PlayerPrefs.GetInt("listMake" + idNhamay + 2));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 2, PlayerPrefs.GetInt("listMake" + idNhamay + 3));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 3, 0);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("listMake" + idNhamay + 1, PlayerPrefs.GetInt("listMake" + idNhamay + 2));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 2, PlayerPrefs.GetInt("listMake" + idNhamay + 3));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 3, 0);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("listMake" + idNhamay + 2, PlayerPrefs.GetInt("listMake" + idNhamay + 3));
                        PlayerPrefs.SetInt("listMake" + idNhamay + 3, 0);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("listMake" + idNhamay + 3, 0);
                        break;
                }
                break;
            }
        }
    }

    //set thu hoạch
    //public bool checkThuhoachSP(int idNhamay, int idsp)
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (PlayerPrefs.GetInt("thuhoach" + idNhamay + i) == idsp)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //public void setThuhoach(int idNhamay, int idsp)
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (PlayerPrefs.GetInt("thuhoach" + idNhamay + i) <= 0)
    //        {
    //            PlayerPrefs.SetInt("thuhoach" + idNhamay + i, idsp);
    //            break;
    //        }
    //    }
    //}
    //public List<int> getListThuhoach(int idNhamay)
    //{
    //    List<int> list = new List<int>();
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (PlayerPrefs.GetInt("thuhoach" + idNhamay + i) > 0)
    //        {
    //            list.Add(PlayerPrefs.GetInt("thuhoach" + idNhamay + i));
    //        }
    //    }
    //    return list;
    //}

    //public void newListThuhoach(int idNhamay)
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        PlayerPrefs.SetInt("thuhoach" + idNhamay + i, 0);
    //    }
    //}

    #endregion 
    //====================Order===========================================
    List<int> listIdvp;
    void newListIdvp()
    {
        //Debug.Log("newListIdvp==================>>>>");
        listIdvp = new List<int>();
        for (int i = 0; i < GameManagerControll.instance.dataNongSan.data.Length; i++)
            if (GameManagerControll.instance.dataNongSan.data[i].levelUnlock <= getLevel())
                if (i < GameManagerControll.instance.idCua && i != GameManagerControll.instance.idBinhtuoinuoc)
                    listIdvp.Add(i);
    }
    public void newListOrder()
    {
        //Debug.Log(" ===========newListOrder=========");
        //thứ tự tên
        PlayerPrefs.SetInt("nameorder", 0);
        newListIdvp();

        for (int i = 0; i < 6; i++)
        {
            int slitem = Random.Range(1, getSlItemOrder());
            //Debug.Log("==========="+i+"============");
            for (int j = 1; j <= slitem; j++)
            {
                int idvp = getIdVpOrder();//random idvp theo các vp đc mở
                int soluong = Random.Range(1, getSoluongVPorder());//random số lượng
                if (idvp >= GameManagerControll.instance.idStarTA && idvp <= GameManagerControll.instance.idEndTA)
                    soluong = Random.Range(1, getSoluongVPThucan());
                //Debug.Log("Soluong["+idvp+"-"+i+"-"+j+"]= " + soluong);
                PlayerPrefs.SetInt("order" + i, j);
                PlayerPrefs.SetInt("order_idvp" + i + j, idvp);
                PlayerPrefs.SetInt("order_soluong" + i + j, soluong);
                PlayerPrefs.SetInt("order_exp" + i, 0);
            }
        }
    }
    public List<ListOrder> getListOrder()
    {
        //Debug.Log("getListOrder==================>>>>");
        List<ListOrder> listOrder = new List<ListOrder>();
        newListIdvp();
        //Debug.Log(" listIdvp = " + listIdvp.Count);

        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("exp" + i, 0);
            List<ItemOrder> itemOrders = new List<ItemOrder>();
            for (int j = 1; j <= PlayerPrefs.GetInt("order" + i); j++)
            {
                ItemOrder item = new ItemOrder(
                PlayerPrefs.GetInt("order_idvp" + i + j),
                PlayerPrefs.GetInt("order_soluong" + i + j));
                if (!checkLap(item, itemOrders))
                    itemOrders.Add(item);
            }

            int exp = PlayerPrefs.GetInt("order_exp" + i);
            if (exp == 0)
            {
                exp = getExpOrder(itemOrders);
                PlayerPrefs.SetInt("order_exp" + i, exp);
            }
            //Debug.Log("exp" + i + "= " + exp);
            int idname = Random.Range(0, listNameV.Count - 1);
            listOrder.Add(new ListOrder(listNameV[idname], listNameE[idname], getCointOrder(itemOrders), exp,
                getKchoanthanhOrder(itemOrders), itemOrders));
        }
        return listOrder;
    }
    public void newOrder(int pos)
    {
        newListIdvp();
        int slitem = Random.Range(1, getSlItemOrder());
        Debug.Log("=======New order====: " + pos + "====");
        PlayerPrefs.SetInt("order_exp" + pos, 0);
        for (int j = 1; j <= slitem; j++)
        {
            int idvp = getIdVpOrder();//random idvp theo các vp đc mở
            int soluong = Random.Range(1, getSoluongVPorder());//random số lượng
            if (idvp >= GameManagerControll.instance.idStarTA && idvp <= GameManagerControll.instance.idEndTA)
                soluong = Random.Range(1, getSoluongVPThucan());
            //if (idvp >= 10 && idvp <= 13) soluong = Random.Range(1, 6);
            PlayerPrefs.SetInt("order" + pos, j);
            PlayerPrefs.SetInt("order_idvp" + pos + j, idvp);
            PlayerPrefs.SetInt("order_soluong" + pos + j, soluong);
        }
    }

    //check lặp item trong 1 order
    bool checkLap(ItemOrder itemOrder, List<ItemOrder> list)
    {
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (itemOrder.idVatphams == list[i].idVatphams)
                count++;
        }
        if (count > 0) return true;
        return false;
    }

    List<string> listNameV = new List<string> {"Chợ xanh", "Nhà hàng","Chợ", "Trường học", "Quán sửa xe", "Tiệm trà sữa", "Trại lợn", "Trại bò", "Trại cừu",
        "Trại gà", "Khách sạn", "Ủy ban","Quầy Bar","Siêu thị","Tạp hóa","Bảo tàng","Quán ăn","Cửa tiệm online","Nhà hát","Nhà ga","Rạp chiếu phim","Rạp xiếc"};
    List<string> listNameE = new List<string> {"Green market", "Restaurant", "Market", "School", "Car repair", "Milk tea shop", "Pig farm", "Cow farm", "Sheep farm",
 "Chicken farm", "Hotel", "Committee", "Bar", "Supermarket", "Grocery", "Museum", "Restaurant", "Shop online","Theatre","Station","Cinema","Circus"};

    //tính coint order
    int getCointOrder(List<ItemOrder> itemOrders)
    {
        int coint = 0;
        for (int j = 0; j < itemOrders.Count; j++)
        {
            coint += GameManagerControll.instance.dataNongSan.data[itemOrders[j].idVatphams].giaban * itemOrders[j].soluong;
        }
        return coint + itemOrders.Count * 2;
    }
    //tính kim cương hoàn thành order
    int getKchoanthanhOrder(List<ItemOrder> itemOrders)
    {
        //Debug.Log("getKchoanthanhOrder: " + itemOrders.Count);
        int kc = 0;
        int temp = 0;
        for (int j = 0; j < itemOrders.Count; j++)
        {
            if (getVpKho(itemOrders[j].idVatphams) < itemOrders[j].soluong)
            {
                temp = itemOrders[j].soluong - getVpKho(itemOrders[j].idVatphams);
                if (temp <= 0) temp = 1;
                kc += GameManagerControll.instance.dataNongSan.data[itemOrders[j].idVatphams].kcpt * temp;
            }
        }
        return kc;
    }
    int getExpOrder(List<ItemOrder> itemOrders)
    {
        int temp = 0;
        temp = getCointOrder(itemOrders) - Random.Range(getCointOrder(itemOrders) / 4, getCointOrder(itemOrders) / 2);
        for (int i = 0; i < itemOrders.Count; i++)
        {
            if (itemOrders[i].idVatphams >= 10)
            {
                temp += itemOrders[i].soluong * getLevel();
            }
        }
        return temp;
    }

    //lấy số lượng item trong 1 order theo level
    int getSlItemOrder()
    {
        if (getLevel() < 3) return 1;
        if (getLevel() == 3) return 2;
        if (getLevel() >= 4 && getLevel() < 7) return 3;
        if (getLevel() >= 7 && getLevel() < 12) return 4;
        if (getLevel() >= 12 && getLevel() < 15) return 5;
        if (getLevel() >= 15) return 6;
        return 1;
    }
    //lấy số lượng vật phẩm từng item theo level
    int getSoluongVPorder()
    {
        if (getLevel() >= 2 && getLevel() < 5) return 4;
        if (getLevel() >= 5 && getLevel() < 10) return 6;
        if (getLevel() >= 10 && getLevel() < 15) return 8;
        if (getLevel() >= 15 && getLevel() < 20) return 10;
        if (getLevel() >= 20) return 12;
        return 2;
    }
    //lấy số lượng vật phẩm thức ăn
    int getSoluongVPThucan()
    {
        if (getLevel() >= 2 && getLevel() < 5) return 2;
        if (getLevel() >= 5 && getLevel() < 10) return 3;
        if (getLevel() >= 10 && getLevel() < 15) return 4;
        if (getLevel() >= 15) return 6;
        return 2;
    }
    //lấy random id vật phẩm theo item đã mở khóa theo level
    int getIdVpOrder()
    {
        int idvp = Random.Range(0, listIdvp.Count);
        return listIdvp[idvp];
    }
    //=================================================================================
    //thành tích
    public void addVpThanhtich(int idvp, int sl)
    {
        PlayerPrefs.SetInt("vpthanhtich" + idvp, sl + getVpThanhtich(idvp));
    }
    public void removeVpThanhtich(int idvp)
    {
        PlayerPrefs.SetInt("vpthanhtich" + idvp, 0);
    }
    public int getVpThanhtich(int idvp)
    {
        return PlayerPrefs.GetInt("vpthanhtich" + idvp);
    }

    public void nhanThanhtich(int id, int value)
    {
        PlayerPrefs.SetInt("thanhtich" + id, value);
    }
    public int getThanhtich(int id)
    {
        return PlayerPrefs.GetInt("thanhtich" + id);
    }

    //==============save obj=================
    public void saveObjLuoi(string nameObj, int idobj, float rotation, int idluoi)
    {
        setObjLuoi(idluoi, 1); //cờ check có vật tại id đó
        //loại vật nào
        PlayerPrefs.SetString("luoinameobj" + idluoi, nameObj);
        //id map trong gamecontroller
        PlayerPrefs.SetInt("luoiidobj" + idluoi, idobj);
        PlayerPrefs.SetFloat("luoirotationobj" + idluoi, rotation);

    }
    public void removeObjLuoi(int idluoi)
    {
        setObjLuoi(idluoi, 0);
        PlayerPrefs.SetString("luoinameobj" + idluoi, "");
        PlayerPrefs.SetInt("luoiidobj" + idluoi, 0);
    }
    public void setObjLuoi(int idluoi, int value)
    {
        PlayerPrefs.SetInt("luoisinhvat" + idluoi, value);
    }

    public int getObjLuoi(int idluoi)
    {
        return PlayerPrefs.GetInt("luoisinhvat" + idluoi);
    }
    public float getRotationLuoi(int idluoi)
    {
        return PlayerPrefs.GetFloat("luoirotationobj" + idluoi);
    }
    public string getTagObjLuoi(int idluoi)
    {
        return PlayerPrefs.GetString("luoinameobj" + idluoi);
    }
    public int getIdObjLuoi(int idluoi)
    {
        return PlayerPrefs.GetInt("luoiidobj" + idluoi);
    }

    //check chỉ cho phép xây nhà máy 1 lần,
    //public void setBuildingObj(string name, int value)
    //{
    //    PlayerPrefs.SetInt("building" + name, value);
    //}
    //public bool getBuildingObj(string name)
    //{
    //    if (PlayerPrefs.GetInt("building" + name) > 0) return true;
    //    return false;
    //}

    public void setBuildingObj(string name, int id, int value)
    {
        PlayerPrefs.SetInt("building" + name + id, value);
    }
    public bool getBuildingObj(string name, int id)
    {
        if (PlayerPrefs.GetInt("building" + name + id) > 0) return true;
        return false;
    }

    public void setLevelUnlockObj(string name, int id, int sl)
    {
        //Debug.Log("setLevelUnlockObj: " + name + id + "= " + sl);
        PlayerPrefs.SetInt(name + id, sl);
    }
    public int getLevelUnlockObj(string name, int id)
    {
        return PlayerPrefs.GetInt(name + id);
    }
    // mua con vật 1 lần
    public void setBuyPets(int id)
    {
        PlayerPrefs.SetInt("pets" + id, 1);
    }
    public bool getBuyPets(int id)
    {
        if (PlayerPrefs.GetInt("pets" + id) == 1) return true;
        return false;
    }
    public void setPetsShow(int value)
    {
        PlayerPrefs.SetInt("pets", value);
    }
    public bool getPetsShow()
    {
        if (PlayerPrefs.GetInt("pets") > 0) return true;
        return false;
    }

    public void setPets(int id)
    {
        PlayerPrefs.SetInt("petsmap", id);
    }
    public int getPets()
    {
        return PlayerPrefs.GetInt("petsmap");
    }

    //sound
    public void setVolume(string name, float vl)
    {
        PlayerPrefs.SetFloat("volume" + name, vl);
    }
    public float getVolume(string name)
    {
        return PlayerPrefs.GetFloat("volume" + name);
    }
    //Giếng
    public void setTimeStarGieng(int id, int value)
    {
        PlayerPrefs.SetInt("gieng" + id, value);
    }
    public int getTimeStarGieng(int id)
    {
        return PlayerPrefs.GetInt("gieng" + id);
    }



    //public void setDanhanThuongngay(int value)
    //{
    //    PlayerPrefs.SetInt("nhanthuongngay", value);
    //}
    //public int getNhanthuongngay()
    //{
    //    return PlayerPrefs.GetInt("nhanthuongngay");
    //}
    //public void setThuongNgay(int day)
    //{
    //    PlayerPrefs.SetInt("thuongngay", day);
    //}
    //public int getThuongngay()
    //{
    //    return PlayerPrefs.GetInt("thuongngay");
    //}
    //phần thưởng hằng ngày
    public void setDay(int day)
    {
        PlayerPrefs.SetInt("day", day);
    }
    public int getDay()
    {
        return PlayerPrefs.GetInt("day");
    }
    public void setDayGame(int day, int month, int year)
    {
        PlayerPrefs.SetInt("daygame" + month + year, day);
        //Debug.Log("setDayGame = " + day);
    }
    public int getDayGame(int month, int year)
    {
        //Debug.Log("getDayGame = " + PlayerPrefs.GetInt("daygame" + month + year));
        return PlayerPrefs.GetInt("daygame" + month + year);
    }
    public void setDayDaNhan(int ngay, int value)
    {
        PlayerPrefs.SetInt("ngaydanhan" + ngay, value);
    }
    public bool checkDayDaNhan(int ngay)
    {
        if (PlayerPrefs.GetInt("ngaydanhan" + ngay) > 0)
            return true;
        return false;
    }

    public void setDayNextThuongngay(int idbtn)
    {
        if (idbtn > 6)
        {
            idbtn = 0;
            for (int i = 0; i < 7; i++)
                PlayerPrefs.SetInt("ngaydanhan" + i, 0);
        }
        PlayerPrefs.SetInt("daythuongngay", idbtn);
    }
    public int getDayNextThuongngay()
    {
        return PlayerPrefs.GetInt("daythuongngay");
    }

    //xóa rác
    public void xoarac(int idmap, int idrac)
    {
        PlayerPrefs.SetInt("xoarac" + idmap + idrac, 1);
    }
    public bool getXoarac(int idmap, int idrac)
    {
        if (PlayerPrefs.GetInt("xoarac" + idmap + idrac) > 0)
            return true;
        return false;
    }

    //check lần đầu xây chuồng
    public void landauxaychuong(string objname, int value)
    {
        PlayerPrefs.SetInt("landau" + objname, value);
    }
    public bool getLandauxaychuong(string objname)
    {
        if (PlayerPrefs.GetInt("landau" + objname) == 0)
            return true;
        return false;
    }

    //SET LƯU HƯỚNG DÂN
    public void setHuongdan(int value)
    {
        PlayerPrefs.SetInt("huongdan", value);
    }
    public bool checkHuongdan()
    {
        if (PlayerPrefs.GetInt("huongdan") == 0)
            return true;
        return false;
    }
    public void setBuocHuongdan(int value)
    {
        PlayerPrefs.SetInt("buochuongdan", value);
    }
    public int getBuocHuongdan()
    {
        return PlayerPrefs.GetInt("buochuongdan");
    }
    //thoát khi chưa hết hướng dẫn
    public void setThoatHD(int value)
    {
        PlayerPrefs.SetInt("thoathuongdan", value);
    }
    public bool getThoatHD()
    {
        if (PlayerPrefs.GetInt("thoathuongdan") > 0)
            return true;
        return false;
    }
    public void setChat(int value)
    {
        PlayerPrefs.SetInt("chat", value);
    }
    public int getChat()
    {
        return PlayerPrefs.GetInt("chat");
    }

    #region khinh khí cầu
    public void setChonhanKC(int vl)
    {
        PlayerPrefs.SetInt("chonhankhicau", vl);
    }
    public bool getChonhanKC()
    {
        if (PlayerPrefs.GetInt("chonhankhicau") > 0) return true;
        return false;
    }
    public void setTimeStartKhicau(int t)
    {
        PlayerPrefs.SetInt("timestartKhicau", t);
    }
    public int getTimeStarKhicau()
    {
        return PlayerPrefs.GetInt("timestartKhicau");
    }
    #endregion
    #region pets
    public void setTimeStartPets(int id, int t)
    {
        Debug.Log("setTimeStartPets" + id);
        PlayerPrefs.SetInt("timestartpets" + id, t);
    }
    public int getTimeStarPets(int id)
    {
        return PlayerPrefs.GetInt("timestartpets" + id);
    }
    public void setTTPets(int id, string tt)
    {
        PlayerPrefs.SetString("ttpets" + id, tt);
    }
    public string getTTPets(int id)
    {
        return PlayerPrefs.GetString("ttpets" + id, "null");
    }
    #endregion
}
