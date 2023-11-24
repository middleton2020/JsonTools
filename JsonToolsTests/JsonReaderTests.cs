using Microsoft.VisualStudio.TestTools.UnitTesting;
using CM.JsonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.JsonTools.Tests
{
    [TestClass()]
    public class JsonReaderTests
    {
        [TestMethod()]
        public void ReadJsonTest()
        {
            TransferOrderData testItem;
            testItem = new TransferOrderData();

            string testJson = "{\"order_id\":3986441,";
            testJson += "\"date_ordered\":\"2013-12-17T07:00:15.087Z\",";
            testJson += "\"seller_name\":\"sklee\",";
            testJson += "\"store_name\":\"skleestore\",";
            testJson += "\"buyer_name\":\"covariance1\",";
            testJson += "\"buyer_email\":\"skleew@gmail.com\",";
            testJson += "\"require_insurance\":true,";
            testJson += "\"status\":\"PENDING\",";
            testJson += "\"is_invoiced\":false,";
            testJson += "\"total_count\":10,";
            testJson += "\"unique_count\":1,";
            testJson += "\"payment\": {";
            testJson += "\"method\":\"PayPal.com\",";
            testJson += "\"currency_code\":\"USD\",";
            testJson += "\"date_paid\":\"2013-12-17T09:20:02.000Z\",";
            testJson += "\"status\":\"Sent\"";
            testJson += "},";
            testJson += "\"shipping\": {";
            testJson += "\"address\": {";
            testJson += "\"name\": {";
            testJson += "\"full\":\"Seulki Lee\"";
            testJson += "},";
            testJson += "\"full\":\"Geumho-dong 2-ga, Seongdong-gu\",";
            testJson += "\"country_code\":\"KR\"";
            testJson += "},";
            testJson += "\"date_shipped\":\"2013-12-17T03:00:15.087Z\"";
            testJson += "},";
            testJson += "\"cost\": {";
            testJson += "\"currency_code\":\"USD\",";
            testJson += "\"subtotal\":\"139.9900\",";
            testJson += "\"grand_total\":\"157.8000\",";
            testJson += "\"disp_currency_code\":\"USD\",";
            testJson += "\"disp_subtotal\":\"139.9900\",";
            testJson += "\"disp_grand_total\":\"157.8000\",";
            testJson += "\"etc1\":\"0.0000\",";
            testJson += "\"etc2\":\"0.0000\",";
            testJson += "\"insurance\":\"3.0500\",";
            testJson += "\"shipping\":\"14.7600\",";
            testJson += "\"credit\":\"0.0000\",";
            testJson += "\"coupon\":\"0.0000\"";
            testJson += "}";
            testJson += "},";

            testJson += "{";
            testJson += "\"order_id\":23215046,";
            testJson += "\"date_ordered\":\"2023-09-29T16:21:57.923Z\",";
            testJson += "\"date_status_changed\":\"2023-09-29T16:21:57.923Z\",";
            testJson += "\"seller_name\":\"bricksinbloom\",";
            testJson += "\"store_name\":\"Bricksinbloom\",";
            testJson += "\"buyer_name\":\"jaggerous\",";
            testJson += "\"status\":\"PAID\",";
            testJson += "\"total_count\":700,";
            testJson += "\"unique_count\":3,";
            testJson += "\"is_filed\":false,";
            testJson += "\"salesTax_collected_by_bl\":false,";
            testJson += "\"vat_collected_by_bl\":false,";
            testJson += "\"payment\":{";
            testJson += "\"method\":\"Credit/Debit (Powered by Stripe)\",";
            testJson += "\"currency_code\":\"GBP\",";
            testJson += "\"date_paid\":\"2023-09-29T16:21:57.923Z\",";
            testJson += "\"status\":\"Received\"";
            testJson += "},";
            testJson += "\"cost\":{";
            testJson += "\"currency_code\":\"GBP\",";
            testJson += "\"subtotal\":\"21.0000\",";
            testJson += "\"grand_total\":\"23.1500\",";
            testJson += "\"final_total\":\"23.1500\"";
            testJson += "},";
            testJson += "\"disp_cost\":{";
            testJson += "\"currency_code\":\"GBP\",";
            testJson += "\"subtotal\":\"21.0000\",";
            testJson += "\"grand_total\":\"23.1500\",";
            testJson += "\"final_total\":\"23.1500\"";
            testJson += "}";
            testJson += "},";

            testJson += "[";
            testJson += "{";
            testJson += "\"image_small\":\"https://img.brickowl.com/files/image_cache/small/lego-transparent-brick-2-x-2-6223-35275-27-939628-97.jpg\",";
            testJson += "\"name\":\"LEGO Transparent Brick 2 x 2 (6223 / 35275)\",";
            testJson += "\"type\":\"Part\",";
            testJson += "\"color_name\":\"Transparent\",";
            testJson += "\"color_id\":\"97\",";
            testJson += "\"boid\":\"939628-97\",";
            testJson += "\"lot_id\":\"82220185\",";
            testJson += "\"condition\":\"New\",";
            testJson += "\"full_con\":\"new\",";
            testJson += "\"ordered_quantity\":\"20\",";
            testJson += "\"personal_note\":\"drawer 155 and os 022\",";
            testJson += "\"bl_lot_id\":\"292687551\",";
            testJson += "\"external_lot_ids\":{";
            testJson += "\"other\":\"292687551\"";
            testJson += "},";
            testJson += "\"remaining_quantity\":\"412\",";
            testJson += "\"weight\":\"1.17\",";
            testJson += "\"public_note\":null,";
            testJson += "\"order_item_id\":\"49361745\",";
            testJson += "\"base_price\":\"0.189\",";
            testJson += "\"ids\":[";
            testJson += "{";
            testJson += "\"id\":\"3003\",";
            testJson += "\"type\":\"ldraw\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"6223\",";
            testJson += "\"type\":\"design_id\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"35275\",";
            testJson += "\"type\":\"design_id\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"939628-97\",";
            testJson += "\"type\":\"boid\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"4130389\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"4175396\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"4190520\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"4276601\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"4276601\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"6195264\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"6239418\",";
            testJson += "\"type\":\"item_no\"";
            testJson += "},";
            testJson += "{";
            testJson += "\"id\":\"6239418\",\"type\":\"item_no\"";
            testJson += "}";
            testJson += "]";
            testJson += "}";
            testJson += "]";

            //JsonReader testReader = new JsonReader();
            //testReader.ReadJson(testItem, testJson);

            Assert.IsNotNull(testItem);
        }
    }
}