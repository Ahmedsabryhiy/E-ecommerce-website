//console.log("Script loaded: ClsItems module");

//var ClsItems = {
//    GetAll: function () {
//        console.log("Entering ClsItems.GetAll function");
//        try {
//            if (typeof Helper === 'undefined' || typeof Helper.AjaxCallPost !== 'function') {
//                throw new Error("Helper or Helper.AjaxCallPost is not defined");
//            }

//            Helper.AjaxCallPost("https://localhost:7226/api/Items", {}, "json",
//                function (data) {
//                    console.log("AjaxCallPost success callback", data);
//                    if (!data || !data.data || !Array.isArray(data.data)) {
//                        console.error("Received data is not in the expected format", data);
//                        return;
//                    }

//                    if (typeof $ === 'undefined' || typeof $.fn.pagination !== 'function') {
//                        console.error("jQuery or pagination plugin is not loaded");
//                        return;
//                    }

//                    $('#ItemPagination').pagination({
//                        dataSource: data.data,
//                        pageSize: 20,
//                        showPrevious: false,
//                        showNext: true,
//                        callback: function (data, pagination) {
//                            console.log("Pagination callback", data, pagination);
//                            var htmlData = data.map(ClsItems.DrawItem).join('');
//                            var itemArea = document.getElementById("ItemArea");
//                            if (itemArea) {
//                                itemArea.innerHTML = htmlData;
//                                console.log("Items rendered to ItemArea");
//                            } else {
//                                console.error("ItemArea element not found");
//                            }
//                        }
//                    });
//                },
//                function (error) {
//                    console.error("AjaxCallPost error callback", error);
//                }
//            );
//        } catch (error) {
//            console.error("Error in ClsItems.GetAll:", error);
//        }
//    },
//    DrawItem: function (item) {
//        console.log("DrawItem called for", item);
//        // ... (rest of the DrawItem function remains the same)
//    }
//};

//try {
//    console.log("Attempting to call ClsItems.GetAll");
//    ClsItems.GetAll();
//    console.log("ClsItems.GetAll called successfully");
//} catch (error) {
//    console.error("Error calling ClsItems.GetAll:", error);
//}


var ClsItems = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7050/api/Items", {}, "json",
            function (data) {


                $('#ItemPagination').pagination({
                    dataSource: data.data,
                    pageSize: 20,
                    showGoInput: true,
                    showGoButton: true,
                    callback: function (data, pagination) {
                        // template method of yourself
                        var htmlData = "";

                        for (var i = 0; i < data.length; i++) {
                            htmlData += ClsItems.DrawItem(data[i]);
                        }

                        var d1 = document.getElementById('ItemArea');
                        d1.innerHTML = htmlData;
                    }
                });
            }, function () { });
    },
    DrawItem: function (item) {
        return `
        <div class='col-xl-3 col-6 col-grid-box'>
            <div class='product-box'>
                <div class='img-wrapper'>
                    <div class='front'>
                        <a href='#'><img src='/Uploads/Items/${item.imageName}' class='img-fluid blur-up lazyload bg-img' alt=''></a>
                    </div>
                    <div class='back'>
                        <a href='#'><img src='/Uploads/Items/${item.imageName}' class='img-fluid blur-up lazyload bg-img' alt=''></a>
                    </div>
                    <div class='cart-info cart-wrap'>
                        <button data-toggle='modal' data-target='#addtocart' title='Add to cart'><i class='ti-shopping-cart'></i></button>
                        <a href='javascript:void(0)' title='Add to Wishlist'><i class='ti-heart' aria-hidden='true'></i></a>
                        <a href='#' data-toggle='modal' data-target='#quick-view' title='Quick View'><i class='ti-search' aria-hidden='true'></i></a>
                        <a href='compare.html' title='Compare'><i class='ti-reload' aria-hidden='true'></i></a>
                    </div>
                </div>
                <div class='product-detail'>
                    <div class='rating'>
                        <i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i>
                    </div>
                    <a href='product-page(no-sidebar).html'><h6>${item.itemName}</h6></a>
                    <h4>$${item.salesPrice}</h4>
                    <ul class='color-variant'>
                        <li class='bg-light0'></li><li class='bg-light1'></li><li class='bg-light2'></li>
                    </ul>
                </div>
            </div>
        </div>
    `;
    }

}
ClsItems.GetAll();
