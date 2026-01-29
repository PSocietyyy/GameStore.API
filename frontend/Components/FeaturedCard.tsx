"use client";
import React from "react";
import LeagueOfLegendsImage from "@/public/images/lol.jpg";
import Image from "next/image";
import Button from "./Button";
import { HeartIcon, PlayIcon, StarIcon } from "lucide-react";
import { motion } from "framer-motion";

const FeaturedCard = () => {
  const genreLists = ["MOBA", "Action", "Multiplayer"];
  const rating = 4.5;
  const price = "Free to Play";
  const originalPrice = null;

  // Animation variants
  const containerVariants = {
    hidden: { opacity: 0, scale: 0.95 },
    visible: {
      opacity: 1,
      scale: 1,
      transition: {
        duration: 0.6,
        ease: "easeOut",
        staggerChildren: 0.1,
      },
    },
  };

  const itemVariants = {
    hidden: { opacity: 0, y: 20 },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        duration: 0.5,
        ease: "easeOut",
      },
    },
  };

  const badgeVariants = {
    hidden: { opacity: 0, scale: 0.8 },
    visible: {
      opacity: 1,
      scale: 1,
      transition: {
        duration: 0.4,
        ease: "easeOut",
      },
    },
  };

  return (
    <motion.div
      className="w-full h-[300px] md:h-[400px] relative rounded-lg overflow-hidden group shadow-xl hover:shadow-2xl transition-shadow duration-300"
      initial="hidden"
      animate="visible"
      variants={containerVariants}
      whileHover={{ scale: 1.01 }}
    >
      {/* Image with overlay gradient */}
      <Image
        src={LeagueOfLegendsImage}
        alt="League of Legends"
        className="w-full h-full object-cover transition-transform duration-300 group-hover:scale-105"
      />
      <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/40 to-transparent" />

      {/* Genre Tags */}
      <motion.div
        className="absolute top-3 left-3 md:top-4 md:left-4 flex items-center gap-2 flex-wrap max-w-[60%]"
        variants={itemVariants}
      >
        {genreLists.map((genre, index) => (
          <motion.span
            key={index}
            className="bg-gray-900/60 backdrop-blur-sm text-white px-2 py-1 md:px-3 md:py-1.5 rounded-md text-xs md:text-sm font-medium border border-white/10"
            variants={badgeVariants}
            whileHover={{
              scale: 1.05,
              backgroundColor: "rgba(17, 24, 39, 0.8)",
            }}
          >
            {genre}
          </motion.span>
        ))}
      </motion.div>

      {/* Rating Badge */}
      <motion.div
        className="absolute top-3 right-3 md:top-4 md:right-4 bg-gray-900/60 backdrop-blur-sm text-white px-2 py-1 md:px-3 md:py-1.5 rounded-md flex items-center gap-1 border border-white/10"
        initial={{ opacity: 0, x: 20 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ delay: 0.3, duration: 0.5 }}
        whileHover={{ scale: 1.05 }}
      >
        <StarIcon size={16} className="fill-yellow-400 text-yellow-400" />
        <span className="text-sm md:text-base font-semibold">{rating}</span>
      </motion.div>

      {/* Discount Badge (if applicable) */}
      {originalPrice && typeof price === "number" && (
        <motion.div
          className="absolute top-14 md:top-16 right-3 md:right-4 bg-red-600 text-white px-2 py-1 rounded-md text-xs md:text-sm font-bold"
          initial={{ opacity: 0, scale: 0 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ delay: 0.4, duration: 0.5, type: "spring" }}
        >
          -{Math.round(((originalPrice - price) / originalPrice) * 100)}%
        </motion.div>
      )}

      {/* Content */}
      <motion.div
        className="absolute bottom-0 left-0 right-0 p-3 md:p-4 lg:p-6"
        variants={itemVariants}
      >
        <div className="flex flex-col gap-2 md:gap-3">
          {/* Title and Description */}
          <motion.div variants={itemVariants}>
            <motion.h2
              className="text-xl md:text-2xl lg:text-3xl font-bold text-white mb-1 md:mb-2 drop-shadow-lg"
              whileHover={{ color: "#ef4444" }}
              transition={{ duration: 0.2 }}
            >
              League of Legends
            </motion.h2>
            <p className="text-xs md:text-sm text-gray-200 line-clamp-2 md:line-clamp-none drop-shadow-md">
              Rasakan sensasi pertempuran epik di dunia Runeterra.
            </p>
          </motion.div>

          {/* Price and Actions */}
          <motion.div
            className="flex items-end justify-between gap-2 flex-wrap"
            variants={itemVariants}
          >
            {/* Price Section */}
            <motion.div
              className="flex flex-col"
              initial={{ opacity: 0, x: -20 }}
              animate={{ opacity: 1, x: 0 }}
              transition={{ delay: 0.5, duration: 0.5 }}
            >
              {originalPrice && (
                <span className="text-xs md:text-sm text-gray-400 line-through">
                  Rp {originalPrice.toLocaleString("id-ID")}
                </span>
              )}
              <span
                className={`text-lg md:text-xl lg:text-2xl font-bold ${
                  typeof price === "string"
                    ? "text-green-400"
                    : originalPrice
                      ? "text-green-400"
                      : "text-white"
                }`}
              >
                {typeof price === "number"
                  ? `Rp ${price.toLocaleString("id-ID")}`
                  : price}
              </span>
            </motion.div>

            {/* Action Buttons */}
            <motion.div
              className="flex items-center gap-2"
              initial={{ opacity: 0, x: 20 }}
              animate={{ opacity: 1, x: 0 }}
              transition={{ delay: 0.6, duration: 0.5 }}
            >
              <motion.div
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
              >
                <Button
                  href="/detail"
                  size="medium"
                  className="text-sm md:text-base flex items-center"
                >
                  <span className="hidden sm:inline">Detail</span>
                  <PlayIcon size={18} className="sm:ml-1" />
                </Button>
              </motion.div>
              <motion.button
                className="p-2 md:p-2.5 bg-gray-800/60 backdrop-blur-sm hover:bg-red-600 text-white rounded-md transition-all duration-200 border border-white/10 hover:border-red-500"
                aria-label="Add to favorites"
                whileHover={{ scale: 1.1, rotate: 5 }}
                whileTap={{ scale: 0.9 }}
              >
                <HeartIcon size={20} />
              </motion.button>
            </motion.div>
          </motion.div>
        </div>
      </motion.div>
    </motion.div>
  );
};

export default FeaturedCard;
