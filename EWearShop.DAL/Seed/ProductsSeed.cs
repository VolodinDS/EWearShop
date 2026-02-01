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

        List<Product> productsToUpdate = initialProducts
            .Where(product =>
                existingProducts.TryGetValue(product.Key, out Product? existingProduct)
                && existingProduct != product.Value)
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
                dbContext.Products.Update(product);
            }
        }

        dbContext.SaveChanges();
    }

    private static Product[] GetInitialProducts(ProductFactory productFactory) =>
    [
        productFactory.Create(
            "Classic Cotton T-Shirt",
            "Soft cotton tee for everyday wear",
            ProductCategory.TShirts,
            1299,
            "RUB",
            "/images/products/tshirt-classic.jpg"),
        productFactory.Create(
            "Graphic Print T-Shirt",
            "Comfortable tee with bold front print",
            ProductCategory.TShirts,
            1499,
            "RUB",
            "/images/products/tshirt-graphic.jpg"),
        productFactory.Create(
            "Oversized Fit T-Shirt",
            "Relaxed oversized tee with dropped shoulders",
            ProductCategory.TShirts,
            1599,
            "RUB",
            "/images/products/tshirt-oversized.jpg"),

        // Hoodies
        productFactory.Create(
            "Fleece Pullover Hoodie",
            "Warm fleece hoodie with kangaroo pocket",
            ProductCategory.Hoodies,
            3499,
            "RUB",
            "/images/products/hoodie-fleece.jpg"),
        productFactory.Create(
            "Zip-Up Hoodie",
            "Full-zip hoodie with soft brushed lining",
            ProductCategory.Hoodies,
            3799,
            "RUB",
            "/images/products/hoodie-zip.jpg"),
        productFactory.Create(
            "Oversized Street Hoodie",
            "Loose fit hoodie for casual streetwear",
            ProductCategory.Hoodies,
            3999,
            "RUB",
            "/images/products/hoodie-oversized.jpg"),

        // Jackets
        productFactory.Create(
            "Leather Biker Jacket",
            "Genuine leather jacket with classic cut",
            ProductCategory.Jackets,
            12999,
            "RUB",
            "/images/products/jacket-leather.jpg"),
        productFactory.Create(
            "Denim Trucker Jacket",
            "Vintage wash denim jacket with metal buttons",
            ProductCategory.Jackets,
            5999,
            "RUB",
            "/images/products/jacket-denim.jpg"),
        productFactory.Create(
            "Lightweight Windbreaker",
            "Packable windbreaker for mild weather",
            ProductCategory.Jackets,
            4499,
            "RUB",
            "/images/products/jacket-windbreaker.jpg"),

        // Pants
        productFactory.Create(
            "Classic Straight Jeans",
            "Straight leg jeans made from sturdy denim",
            ProductCategory.Pants,
            4599,
            "RUB",
            "/images/products/pants-jeans.jpg"),
        productFactory.Create(
            "Slim Fit Chinos",
            "Stretch chinos with clean tailored look",
            ProductCategory.Pants,
            3999,
            "RUB",
            "/images/products/pants-chinos.jpg"),
        productFactory.Create(
            "Utility Cargo Pants",
            "Cargo pants with multiple pockets and tapered leg",
            ProductCategory.Pants,
            4299,
            "RUB",
            "/images/products/pants-cargo.jpg"),

        // Shorts
        productFactory.Create(
            "Sport Training Shorts",
            "Lightweight shorts for workouts and runs",
            ProductCategory.Shorts,
            1799,
            "RUB",
            "/images/products/shorts-sport.jpg"),
        productFactory.Create(
            "Denim Cutoff Shorts",
            "Casual denim shorts with raw hem",
            ProductCategory.Shorts,
            2199,
            "RUB",
            "/images/products/shorts-denim.jpg"),
        productFactory.Create(
            "Chino City Shorts",
            "Classic chino shorts with slim silhouette",
            ProductCategory.Shorts,
            2499,
            "RUB",
            "/images/products/shorts-chino.jpg"),

        // Dresses
        productFactory.Create(
            "Summer Floral Dress",
            "Breezy cotton dress with floral print",
            ProductCategory.Dresses,
            3299,
            "RUB",
            "/images/products/dress-summer.jpg"),
        productFactory.Create(
            "Wrap Midi Dress",
            "Elegant wrap dress with adjustable waist",
            ProductCategory.Dresses,
            3699,
            "RUB",
            "/images/products/dress-wrap.jpg"),
        productFactory.Create(
            "Knit Bodycon Dress",
            "Soft knit dress with bodycon fit",
            ProductCategory.Dresses,
            3499,
            "RUB",
            "/images/products/dress-knit.jpg"),

        // Skirts
        productFactory.Create(
            "Silk Midi Skirt",
            "Smooth silk skirt with gentle sheen",
            ProductCategory.Skirts,
            2899,
            "RUB",
            "/images/products/skirt-midi.jpg"),
        productFactory.Create(
            "Pleated Chiffon Skirt",
            "Flowy pleated skirt with elastic waistband",
            ProductCategory.Skirts,
            2699,
            "RUB",
            "/images/products/skirt-pleated.jpg"),
        productFactory.Create(
            "Denim Mini Skirt",
            "Classic denim mini skirt with front buttons",
            ProductCategory.Skirts,
            2399,
            "RUB",
            "/images/products/skirt-denim.jpg"),

        // Swimwear
        productFactory.Create(
            "One-Piece Swimsuit",
            "Sleek one-piece with supportive fit",
            ProductCategory.Swimwear,
            2499,
            "RUB",
            "/images/products/swimwear-onepiece.jpg"),
        productFactory.Create(
            "Bikini Swim Set",
            "Two-piece bikini with adjustable straps",
            ProductCategory.Swimwear,
            2299,
            "RUB",
            "/images/products/swimwear-bikini.jpg"),
        productFactory.Create(
            "High-Waist Swim Bottoms",
            "High-rise bottoms with smooth finish",
            ProductCategory.Swimwear,
            1999,
            "RUB",
            "/images/products/swimwear-highwaist.jpg"),

        // Activewear
        productFactory.Create(
            "Yoga Leggings",
            "Stretch leggings with high-rise waistband",
            ProductCategory.Activewear,
            2199,
            "RUB",
            "/images/products/activewear-leggings.jpg"),
        productFactory.Create(
            "Training Sports Bra",
            "Medium support sports bra for gym sessions",
            ProductCategory.Activewear,
            1999,
            "RUB",
            "/images/products/activewear-bra.jpg"),
        productFactory.Create(
            "Running Performance Shorts",
            "Moisture-wicking shorts with inner liner",
            ProductCategory.Activewear,
            1899,
            "RUB",
            "/images/products/activewear-shorts.jpg"),

        // Underwear
        productFactory.Create(
            "Cotton Underwear Set",
            "Soft cotton underwear set for daily comfort",
            ProductCategory.Underwear,
            1599,
            "RUB",
            "/images/products/underwear-set.jpg"),
        productFactory.Create(
            "Seamless Briefs",
            "No-show briefs with smooth bonded edges",
            ProductCategory.Underwear,
            1299,
            "RUB",
            "/images/products/underwear-briefs.jpg"),
        productFactory.Create(
            "Lace Bralette",
            "Lightweight bralette with delicate lace",
            ProductCategory.Underwear,
            1499,
            "RUB",
            "/images/products/underwear-bralette.jpg"),

        // Sleepwear
        productFactory.Create(
            "Cotton Pajama Set",
            "Soft pajama set for restful sleep",
            ProductCategory.Sleepwear,
            2299,
            "RUB",
            "/images/products/sleepwear-pajama.jpg"),
        productFactory.Create(
            "Satin Nightgown",
            "Smooth satin nightgown with adjustable straps",
            ProductCategory.Sleepwear,
            2599,
            "RUB",
            "/images/products/sleepwear-nightgown.jpg"),
        productFactory.Create(
            "Cozy Lounge Robe",
            "Warm lounge robe with belt tie and pockets",
            ProductCategory.Sleepwear,
            2799,
            "RUB",
            "/images/products/sleepwear-robe.jpg"),

        // Accessories
        productFactory.Create(
            "Leather Belt",
            "Classic leather belt with metal buckle",
            ProductCategory.Accessories,
            1899,
            "RUB",
            "/images/products/accessories-belt.jpg"),
        productFactory.Create(
            "Canvas Tote Bag",
            "Roomy tote bag for everyday essentials",
            ProductCategory.Accessories,
            1699,
            "RUB",
            "/images/products/accessories-tote.jpg"),
        productFactory.Create(
            "Wool Knit Beanie",
            "Warm knit beanie for cold days",
            ProductCategory.Accessories,
            1199,
            "RUB",
            "/images/products/accessories-beanie.jpg"),

        // Footwear
        productFactory.Create(
            "Running Sneakers",
            "Lightweight sneakers for daily training",
            ProductCategory.Footwear,
            6999,
            "RUB",
            "/images/products/footwear-sneakers.jpg"),
        productFactory.Create(
            "Leather Ankle Boots",
            "Durable ankle boots with cushioned insole",
            ProductCategory.Footwear,
            8999,
            "RUB",
            "/images/products/footwear-boots.jpg"),
        productFactory.Create(
            "Canvas Slip-On Shoes",
            "Easy slip-on shoes with breathable canvas",
            ProductCategory.Footwear,
            4999,
            "RUB",
            "/images/products/footwear-slipon.jpg"),
    ];
}