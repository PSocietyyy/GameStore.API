"use client";
import React from "react";
import Image, { StaticImageData } from "next/image";
import Button from "./Button";
import { HeartIcon, StarIcon } from "lucide-react";
import { motion } from "framer-motion";

interface ProductCardProps {
  title: string;
  description: string;
  price: number | string;
  originalPrice?: number | null;
  genres: string[];
  developer: string;
  rating: number;
  image: StaticImageData | string;
  href: string;
}

const ProductCard = ({
  title,
  description,
  price,
  originalPrice = null,
  genres,
  developer,
  rating,
  image,
  href,
}: ProductCardProps) => {
  const displayGenre = genres.length > 1 ? `${genres[0]}...` : genres[0];
  const hasDiscount = originalPrice && typeof price === "number";

  return (
    <motion.div
      className="bg-gray-900 rounded-lg overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 group"
      whileHover={{ y: -8, scale: 1.02 }}
      transition={{ duration: 0.3, ease: "easeOut" }}
    >
      {/* Image Container */}
      <div className="relative h-48 md:h-56 overflow-hidden">
        <Image
          src={image}
          alt={title}
          className="w-full h-full object-cover transition-transform duration-300 group-hover:scale-110"
        />
        
        {/* Hover Overlay - Only on Desktop */}
        <motion.div
          className="hidden md:block absolute inset-0 bg-black/80 p-4"
          initial={{ opacity: 0 }}
          whileHover={{ opacity: 1 }}
          transition={{ duration: 0.3 }}
        >
          <p className="text-white text-sm leading-relaxed line-clamp-6">
            {description}
          </p>
        </motion.div>

        {/* Rating Badge */}
        <motion.div
          className="absolute top-3 right-3 bg-gray-900/80 backdrop-blur-sm text-white px-2 py-1 rounded-md flex items-center gap-1"
          initial={{ opacity: 0, x: 20 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ delay: 0.2, duration: 0.4 }}
        >
          <StarIcon size={14} className="fill-yellow-400 text-yellow-400" />
          <span className="text-sm font-semibold">{rating}</span>
        </motion.div>

        {/* Discount Badge */}
        {hasDiscount && (
          <motion.div
            className="absolute top-3 left-3 bg-red-600 text-white px-2 py-1 rounded-md text-xs font-bold"
            initial={{ opacity: 0, scale: 0.8 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ delay: 0.1, duration: 0.4 }}
          >
            -{Math.round(((originalPrice - price) / originalPrice) * 100)}%
          </motion.div>
        )}

        {/* Favorite Button */}
        <motion.button
          className="absolute bottom-3 right-3 p-2 bg-gray-900/60 backdrop-blur-sm hover:bg-red-600 text-white rounded-md transition-all duration-200 opacity-0 group-hover:opacity-100 md:opacity-100"
          aria-label="Add to favorites"
          whileHover={{ scale: 1.1 }}
          whileTap={{ scale: 0.95 }}
        >
          <HeartIcon size={18} />
        </motion.button>
      </div>

      {/* Content */}
      <motion.div
        className="p-4 space-y-3"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.2, duration: 0.5 }}
      >
        {/* Title */}
        <h3 className="text-white font-bold text-lg line-clamp-1 group-hover:text-red-500 transition-colors">
          {title}
        </h3>

        {/* Description - Mobile Only */}
        <p className="md:hidden text-gray-400 text-sm line-clamp-2">
          {description}
        </p>

        {/* Genre and Developer */}
        <div className="flex items-center justify-between text-sm">
          <span className="text-gray-400">
            <span className="text-red-500 font-medium">{displayGenre}</span>
          </span>
          <span className="text-gray-500 text-xs line-clamp-1 max-w-[50%]">
            {developer}
          </span>
        </div>

        {/* Price Section */}
        <div className="flex items-end justify-between">
          <div className="flex flex-col">
            {originalPrice && (
              <motion.span
                className="text-xs text-gray-500 line-through"
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: 0.3 }}
              >
                Rp {originalPrice.toLocaleString("id-ID")}
              </motion.span>
            )}
            <motion.span
              className={`text-lg font-bold ${
                typeof price === "string"
                  ? "text-green-400"
                  : hasDiscount
                  ? "text-green-400"
                  : "text-white"
              }`}
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 0.4, duration: 0.4 }}
            >
              {typeof price === "number"
                ? `Rp ${price.toLocaleString("id-ID")}`
                : price}
            </motion.span>
          </div>
        </div>

        {/* Action Button */}
        <motion.div
          whileHover={{ scale: 1.02 }}
          whileTap={{ scale: 0.98 }}
        >
          <Button href={href} variant="primary" size="medium" className="w-full">
            Lihat Detail
          </Button>
        </motion.div>
      </motion.div>
    </motion.div>
  );
};

export default ProductCard;