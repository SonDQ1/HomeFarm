using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageControll : MonoBehaviour
{
   
    private void OnEnable()
    {
        
        string txt = gameObject.GetComponents<Text>()[0].text;
        if (Until.checkLanguage())
            switch (txt)
            {
                case "Achievement":
                    gameObject.GetComponents<Text>()[0].text = "Thành tích";
                    break;
                case "All":
                    gameObject.GetComponents<Text>()[0].text = "Tất cả";
                    break;
                case "Materials":
                    gameObject.GetComponents<Text>()[0].text = "Nông sản";
                    break;
                case "Productrs":
                    gameObject.GetComponents<Text>()[0].text = "Sản phẩm";
                    break;
                case "No material":
                case "No product":
                    gameObject.GetComponents<Text>()[0].text = "Không có sản phẩm";
                    break;
                case "Orders":
                    gameObject.GetComponents<Text>()[0].text = "Đơn ";
                    break;
                case "Reward":
                    gameObject.GetComponents<Text>()[0].text = "Phần thưởng";
                    break;
                case "Finish":
                    gameObject.GetComponents<Text>()[0].text = "Hoàn thành";
                    break;
                case "Yes":
                    gameObject.GetComponents<Text>()[0].text = "Có";
                    break;
                case "Delivery":
                    gameObject.GetComponents<Text>()[0].text = "Chuyển";
                    break;
                case "Setting":
                    gameObject.GetComponents<Text>()[0].text = "Cài đặt";
                    break;
                case "Building":
                    gameObject.GetComponents<Text>()[0].text = "Xây dựng";
                    break;
                case "Pets":
                    gameObject.GetComponents<Text>()[0].text = "Con vật";
                    break;
                case "Decoration":
                    gameObject.GetComponents<Text>()[0].text = "Trang trí";
                    break;
                case "Fences":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào";
                    break;
                case "Items":
                    gameObject.GetComponents<Text>()[0].text = "Vật phẩm";
                    break;
                case "Daily":
                    gameObject.GetComponents<Text>()[0].text = "Quà tặng";
                    break;
                case "Open cultivated land with:":
                    gameObject.GetComponents<Text>()[0].text = "Mở đất canh tác với:";
                    break;
                case "Congratulations to level up":
                    gameObject.GetComponents<Text>()[0].text = "Chúc mừng bạn lên cấp";
                    break;
                case "Rewards:":
                    gameObject.GetComponents<Text>()[0].text = "Phần thưởng:";
                    break;
                case "Exit game":
                    gameObject.GetComponents<Text>()[0].text = "Thoát trò chơi";
                    break;
                case "Level Up":
                    gameObject.GetComponents<Text>()[0].text = "Lên cấp";
                    break;
                case "Receive":
                    gameObject.GetComponents<Text>()[0].text = "Nhận";
                    break;
                case "Received":
                    gameObject.GetComponents<Text>()[0].text = "Đã nhận";
                    break;
                case "Open box containing products with:":
                    gameObject.GetComponents<Text>()[0].text = "Mở ô chứa sản phẩm sản xuất với:";
                    break;
                case "Complete the order with:":
                    gameObject.GetComponents<Text>()[0].text = "Hoàn thành đơn hàng với:";
                    break;
                case "Expand cultivation land with:":
                    gameObject.GetComponents<Text>()[0].text = "Mở rộng đất canh tác với:";
                    break;
                case "Do you want to exit game?":
                    gameObject.GetComponents<Text>()[0].text = "Bạn muốn thoát trò chơi?";
                    break;
                case "Want to watch video to receive gifts?":
                    gameObject.GetComponents<Text>()[0].text = "Bạn muốn xem video nhận quà?";
                    break;
                case "Congratulations on receiving:":
                    gameObject.GetComponents<Text>()[0].text = "Chúc mừng bạn nhận được:";
                    break;
                    //nhà máy
                case "Animal food":
                    gameObject.GetComponents<Text>()[0].text = "Nhà máy thức ăn";
                    break;
                case "Wells":
                    gameObject.GetComponents<Text>()[0].text = "Giếng nước";
                    break;
                case "Henhouse":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng gà";
                    break;
                case "Pigsty":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng lợn";
                    break;
                case "Factory":
                    gameObject.GetComponents<Text>()[0].text = "Nhà máy";
                    break;
                case "Cowhouse":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng bò";
                    break;
                case "Dairy house":
                    gameObject.GetComponents<Text>()[0].text = "Nhà máy sữa";
                    break;
                case "Sheephouse":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng cừu";
                    break;
                case "Bakehouse":
                    gameObject.GetComponents<Text>()[0].text = "Nhà máy nướng";
                    break;
                case "Beverage house":
                    gameObject.GetComponents<Text>()[0].text = "Nhà máy đồ uống";
                    break;
                case "Tailor house":
                    gameObject.GetComponents<Text>()[0].text = "Xưởng may";
                    break;
                case "Goat barn":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng Dê";
                    break;
                case "Cooking house":
                    gameObject.GetComponents<Text>()[0].text = "Nhà đầu bếp";
                    break;
                case "Brown Henhouse":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng Gà nâu";
                    break;
                case "BBQ house":
                    gameObject.GetComponents<Text>()[0].text = "Nhà nướng thịt";
                    break;
                case "Cow brown barn":
                    gameObject.GetComponents<Text>()[0].text = "Chuồng bò nâu";
                    break;
                case "Craft house":
                    gameObject.GetComponents<Text>()[0].text = "Nhà thủ công";
                    break;
                //pest
                case "Dalmatian":
                    gameObject.GetComponents<Text>()[0].text = "Chó đốm";
                    break;
                case "Golden":
                    gameObject.GetComponents<Text>()[0].text = "Chó vàng";
                    break;
                case "Poodle":
                    gameObject.GetComponents<Text>()[0].text = "Chó xù";
                    break;
                case "Rottweiler":
                    gameObject.GetComponents<Text>()[0].text = "Chó mực";
                    break;
                    //trang trí
                case "Watering-can":
                    gameObject.GetComponents<Text>()[0].text = "Ô doa";
                    break;
                case "Bench":
                    gameObject.GetComponents<Text>()[0].text = "Ghế dài";
                    break;
                case "Lamppost 1":
                    gameObject.GetComponents<Text>()[0].text = "Cột đèn 1";
                    break;
                case "Lamppost 2":
                    gameObject.GetComponents<Text>()[0].text = "Cột đèn 2";
                    break;
                case "Picnic meal":
                    gameObject.GetComponents<Text>()[0].text = "Đồ ăn picnic";
                    break;
                case "Telescope":
                    gameObject.GetComponents<Text>()[0].text = "Kính thiên văn";
                    break;
                case "Decor Tree":
                    gameObject.GetComponents<Text>()[0].text = "Cây cảnh";
                    break;
                case "Mushroom":
                    gameObject.GetComponents<Text>()[0].text = "Nấm";
                    break;
                case "Mini flower-bed":
                    gameObject.GetComponents<Text>()[0].text = "Bồn hoa";
                    break;
                case "Flower vase 1":
                    gameObject.GetComponents<Text>()[0].text = "Bình hoa 1";
                    break;
                case "Puppet":
                    gameObject.GetComponents<Text>()[0].text = "Bù nhìn";
                    break;
                case "Flower decoration":
                    gameObject.GetComponents<Text>()[0].text = "Cột đèn hoa";
                    break;
                case "Fountain":
                    gameObject.GetComponents<Text>()[0].text = "Thác nước";
                    break;
                case "Flower vase 2":
                    gameObject.GetComponents<Text>()[0].text = "Bình hoa 2";
                    break;
                case "Sun loungers":
                    gameObject.GetComponents<Text>()[0].text = "Ghế tắm nắng";
                    break;
                case "Flower gate":
                    gameObject.GetComponents<Text>()[0].text = "Cổng hoa";
                    break;
                case "Fence 1":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào 1";
                    break;
                case "Fence 2":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào 2";
                    break;
                case "Fence 3":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào 3";
                    break;
                case "Fence 4":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào 4";
                    break;
                case "Fence 5":
                    gameObject.GetComponents<Text>()[0].text = "Hàng rào 5";
                    break;
                case "Road 1":
                    gameObject.GetComponents<Text>()[0].text = "Gạch nền 1";
                    break;
                case "Road 2":
                    gameObject.GetComponents<Text>()[0].text = "Gạch nền 2";
                    break;
                case "Hand saw":
                    gameObject.GetComponents<Text>()[0].text = "Cưa";
                    break;
                case "Dynamite":
                    gameObject.GetComponents<Text>()[0].text = "Thuốc nổ";
                    break;
                case "Shovel":
                    gameObject.GetComponents<Text>()[0].text = "Xẻng xúc";
                    break;
                case "Secateurs":
                    gameObject.GetComponents<Text>()[0].text = "Kéo cắt cỏ";
                    break;
                case "-25% time":
                    gameObject.GetComponents<Text>()[0].text = "-25% thời gian";
                    break;
                case "Thank You!":
                    gameObject.GetComponents<Text>()[0].text = "Cám ơn bạn!";
                    break;
                case "Day 1":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 1";
                    break;
                case "Day 2":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 2";
                    break;
                case "Day 3":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 3";
                    break;
                case "Day 4":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 4";
                    break;
                case "Day 5":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 5";
                    break;
                case "Day 6":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 6";
                    break;
                case "Day 7":
                    gameObject.GetComponents<Text>()[0].text = "Ngày 7";
                    break;
                case "Use diamonds to buy items?":
                    gameObject.GetComponents<Text>()[0].text = "Dùng kim cương để mua vật phẩm?";
                    break;
                case "Hello! My name is Anna, welcome to the Happy Farm.":
                    gameObject.GetComponents<Text>()[0].text = "Chào bạn! Tôi tên là Anna, chào mừng bạn đến với Nông trại vui vẻ.";
                    break;
                case "Berger":
                    gameObject.GetComponents<Text>()[0].text = "Chó Berger";
                    break;
                case "Husky":
                    gameObject.GetComponents<Text>()[0].text = "Chó Husky";
                    break;
            }
    }
}
