using EWearShop.Domain.Products;

namespace EWearShop.DAL.Seed;

internal static class ProductsSeed
{
    internal static void Seed(EWearShopDbContext dbContext, ProductFactory productFactory, TimeProvider timeProvider)
    {
        Dictionary<Guid, Product> existingProducts = dbContext.Products.ToDictionary(k => k.Id);
        Dictionary<Guid, Product> initialProducts = GetInitialProducts(productFactory).ToDictionary(k => k.Id);

        List<Product> productsToAdd = initialProducts
            .Where(product => !existingProducts.ContainsKey(product.Key))
            .Select(product => product.Value)
            .ToList();

        List<Product> productsToRemove = existingProducts
            .Where(product => !initialProducts.ContainsKey(product.Key))
            .Select(product => product.Value)
            .ToList();

        List<Product> productsToUpdate = existingProducts
            .Where(product =>
                initialProducts.TryGetValue(product.Key, out Product? existingProduct)
                && (!string.Equals(existingProduct.Name, product.Value.Name)
                    || !string.Equals(existingProduct.Description, product.Value.Description)
                    || existingProduct.Category != product.Value.Category
                    || existingProduct.Price != product.Value.Price
                    || !string.Equals(existingProduct.Currency, product.Value.Currency)
                    || !string.Equals(existingProduct.ImageUrl, product.Value.ImageUrl)))
            .Select(product => product.Value)
            .ToList();

        if (productsToAdd.Count > 0)
        {
            dbContext.Products.AddRange(productsToAdd);
        }

        if (productsToRemove.Count > 0)
        {
            foreach (Product product in productsToRemove)
            {
                product.IsDeleted = true;
                product.DeletedAt = timeProvider.GetUtcNow();
            }
        }

        if (productsToUpdate.Count > 0)
        {
            foreach (Product product in productsToUpdate)
            {
                product.UpdatedAt = timeProvider.GetUtcNow();
                dbContext.Products.Entry(product).CurrentValues.SetValues(initialProducts[product.Id]);
            }
        }

        dbContext.SaveChanges();
    }

    private static Product[] GetInitialProducts(ProductFactory productFactory) =>
    [
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7041-8788-E8F2EF952970"u8),
            "Classic Cotton T-Shirt",
            "Soft cotton tee for everyday wear",
            ProductCategory.TShirts,
            1299,
            "RUB",
            "/images/products/tshirt-classic.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-704D-88B4-0C77B8DF956A"u8),
            "Graphic Print T-Shirt",
            "Comfortable tee with bold front print",
            ProductCategory.TShirts,
            1499,
            "RUB",
            "/images/products/tshirt-graphic.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7078-806C-8748E41AB60F"u8),
            "Oversized Fit T-Shirt",
            "Relaxed oversized tee with dropped shoulders",
            ProductCategory.TShirts,
            1599,
            "RUB",
            "/images/products/tshirt-oversized.png"),

        // Hoodies
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7156-92F5-94D1A6F518AC"u8),
            "Fleece Pullover Hoodie",
            "Warm fleece hoodie with kangaroo pocket",
            ProductCategory.Hoodies,
            3499,
            "RUB",
            "/images/products/hoodie-fleece.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-716C-A524-A746A9AB2719"u8),
            "Zip-Up Hoodie",
            "Full-zip hoodie with soft brushed lining",
            ProductCategory.Hoodies,
            3799,
            "RUB",
            "/images/products/hoodie-zip.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-71CA-829A-E5A5607FDBA4"u8),
            "Oversized Street Hoodie",
            "Loose fit hoodie for casual streetwear",
            ProductCategory.Hoodies,
            3999,
            "RUB",
            "/images/products/hoodie-oversized.png"),

        // Jackets
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7289-8445-2C7FEA8973E4"u8),
            "Leather Biker Jacket",
            "Genuine leather jacket with classic cut",
            ProductCategory.Jackets,
            12999,
            "RUB",
            "/images/products/jacket-leather.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-729F-A609-86A4A0163B00"u8),
            "Denim Trucker Jacket",
            "Vintage wash denim jacket with metal buttons",
            ProductCategory.Jackets,
            5999,
            "RUB",
            "/images/products/jacket-denim.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-72CB-81F8-95F6F7AD78E1"u8),
            "Lightweight Windbreaker",
            "Packable windbreaker for mild weather",
            ProductCategory.Jackets,
            4499,
            "RUB",
            "/images/products/jacket-windbreaker.png"),

        // Pants
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7402-9ED2-330F82968E1A"u8),
            "Classic Straight Jeans",
            "Straight leg jeans made from sturdy denim",
            ProductCategory.Pants,
            4599,
            "RUB",
            "/images/products/pants-jeans.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7425-ACE4-2BFB14F54433"u8),
            "Slim Fit Chinos",
            "Stretch chinos with clean tailored look",
            ProductCategory.Pants,
            3999,
            "RUB",
            "/images/products/pants-chinos.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7548-872A-D30A526DE4F4"u8),
            "Utility Cargo Pants",
            "Cargo pants with multiple pockets and tapered leg",
            ProductCategory.Pants,
            4299,
            "RUB",
            "/images/products/pants-cargo.png"),

        // Shorts
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-757F-B72B-A99EB4A57C4D"u8),
            "Sport Training Shorts",
            "Lightweight shorts for workouts and runs",
            ProductCategory.Shorts,
            1799,
            "RUB",
            "/images/products/shorts-sport.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-75DD-B086-75EF9DD52D35"u8),
            "Denim Cutoff Shorts",
            "Casual denim shorts with raw hem",
            ProductCategory.Shorts,
            2199,
            "RUB",
            "/images/products/shorts-denim.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7675-A524-287AD9F0EE58"u8),
            "Chino City Shorts",
            "Classic chino shorts with slim silhouette",
            ProductCategory.Shorts,
            2499,
            "RUB",
            "/images/products/shorts-chino.png"),

        // Activewear
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7B6A-A2D1-A5293A15CD3F"u8),
            "Running Performance Shorts",
            "Moisture-wicking shorts with inner liner",
            ProductCategory.Activewear,
            1899,
            "RUB",
            "/images/products/activewear-shorts.png"),

        // Underwear
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7C12-B19F-38A3D4BC993F"u8),
            "Cotton Underwear Set",
            "Soft cotton underwear set for daily comfort",
            ProductCategory.Underwear,
            1599,
            "RUB",
            "/images/products/underwear-set.png"),

        // Sleepwear
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7DFC-9108-E411E18D955E"u8),
            "Cotton Pajama Set",
            "Soft pajama set for restful sleep",
            ProductCategory.Sleepwear,
            2299,
            "RUB",
            "/images/products/sleepwear-pajama.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7ED9-8AB3-5AC517862CAE"u8),
            "Cozy Lounge Robe",
            "Warm lounge robe with belt tie and pockets",
            ProductCategory.Sleepwear,
            2799,
            "RUB",
            "/images/products/sleepwear-robe.png"),

        // Accessories
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7EEA-9D57-CDD882DC7692"u8),
            "Leather Belt",
            "Classic leather belt with metal buckle",
            ProductCategory.Accessories,
            1899,
            "RUB",
            "/images/products/accessories-belt.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7F4D-AD3E-F95B1224E27A"u8),
            "Wool Knit Beanie",
            "Warm knit beanie for cold days",
            ProductCategory.Accessories,
            1199,
            "RUB",
            "/images/products/accessories-beanie.png"),

        // Footwear
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7FA8-BE40-E858DC9AE5AC"u8),
            "Running Sneakers",
            "Lightweight sneakers for daily training",
            ProductCategory.Footwear,
            6999,
            "RUB",
            "/images/products/footwear-sneakers.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7FC3-A160-0B8E318EF351"u8),
            "Leather Ankle Boots",
            "Durable ankle boots with cushioned insole",
            ProductCategory.Footwear,
            8999,
            "RUB",
            "/images/products/footwear-boots.png"),
        productFactory.Create(
            Guid.Parse("019C1A1A-8626-7FEA-9D31-6DE92BB1ED2D"u8),
            "Canvas Slip-On Shoes",
            "Easy slip-on shoes with breathable canvas",
            ProductCategory.Footwear,
            4999,
            "RUB",
            "/images/products/footwear-slipon.png"),
    ];
}