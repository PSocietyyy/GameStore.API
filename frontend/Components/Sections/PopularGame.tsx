"use client";
import React from "react";
import ProductCard from "@/Components/ProductCard";
import LeagueOfLegendsImage from "@/public/images/lol.jpg";
import EldenRingImage from "@/public/images/eldenRing.jpg";
import CyberpunkImage from "@/public/images/cyberPunk.jpg";
import ValorantImage from "@/public/images/valorant.jpg";
import Witcher3 from "@/public/images/theWitcher3.jpg";
import MinecraftImage from "@/public/images/miecraft.jpg";
import { motion } from "framer-motion";

const PopularGame = () => {
  const products = [
    {
      id: 1,
      title: "League of Legends",
      description:
        "Bergabunglah dengan jutaan pemain di seluruh dunia dalam pertempuran 5v5 yang intens. Pilih dari lebih dari 160 champion unik, kuasai strategi tim, dan panjat peringkat kompetitif menuju puncak.",
      price: 249000,
      originalPrice: 300000,
      genres: ["MOBA", "Action", "Strategy", "Multiplayer"],
      developer: "Riot Games",
      rating: 4.5,
      image: LeagueOfLegendsImage,
      href: "/game/lol",
    },
    {
      id: 2,
      title: "Elden Ring",
      description:
        "Jelajahi Lands Between yang luas dan misterius. Hadapi musuh yang menantang, temukan rahasia tersembunyi, dan ciptakan legenda Anda sendiri di dunia yang diciptakan oleh FromSoftware dan George R.R. Martin.",
      price: 699000,
      genres: ["RPG", "Action", "Open World", "Dark Fantasy"],
      developer: "FromSoftware",
      rating: 4.8,
      image: EldenRingImage,
      href: "/game/elden-ring",
    },
    {
      id: 3,
      title: "Cyberpunk 2077",
      description:
        "Masuki Night City, kota masa depan yang dipenuhi kekuasaan, glamor, dan modifikasi tubuh. Jadilah V, seorang mercenary yang mencari implan unik yang menjadi kunci keabadian.",
      price: 399000,
      originalPrice: 799000,
      genres: ["RPG", "Action", "Futuristic", "Open World"],
      developer: "CD Projekt Red",
      rating: 4.2,
      image: CyberpunkImage,
      href: "/game/cyberpunk",
    },
    {
      id: 4,
      title: "Valorant",
      description:
        "Tactical shooter 5v5 berbasis karakter yang menggabungkan kemampuan unik dengan strategi tim. Pilih agent favorit Anda dan tunjukkan skill aim serta teamwork terbaik Anda.",
      price: "Free to Play",
      genres: ["FPS", "Tactical", "Competitive"],
      developer: "Riot Games",
      rating: 4.6,
      image: ValorantImage,
      href: "/game/valorant",
    },
    {
      id: 5,
      title: "The Witcher 3",
      description:
        "Ikuti kisah Geralt of Rivia dalam petualangan epik mencari anak angkatnya yang hilang. Jelajahi dunia fantasi yang luas, buat keputusan moral yang berdampak, dan hadapi monster berbahaya.",
      price: 149000,
      originalPrice: 299000,
      genres: ["RPG", "Action", "Fantasy", "Story Rich"],
      developer: "CD Projekt Red",
      rating: 4.9,
      image: Witcher3,
      href: "/game/witcher3",
    },
    {
      id: 6,
      title: "Minecraft",
      description:
        "Bangun, eksplorasi, dan bertahan hidup di dunia yang terbuat dari balok. Dari mode kreatif hingga survival, kemungkinan dalam game sandbox ini tidak terbatas.",
      price: 129000,
      genres: ["Sandbox", "Adventure", "Creative", "Multiplayer"],
      developer: "Mojang Studios",
      rating: 4.7,
      image: MinecraftImage,
      href: "/game/minecraft",
    },
  ];

  // Animation variants for container
  const containerVariants = {
    hidden: { opacity: 0 },
    visible: {
      opacity: 1,
      transition: {
        staggerChildren: 0.1,
      },
    },
  };

  // Animation variants for header
  const headerVariants = {
    hidden: { opacity: 0, y: -20 },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        duration: 0.6,
        ease: "easeOut",
      },
    },
  };

  // Animation variants for each card
  const cardVariants = {
    hidden: { opacity: 0, y: 20, scale: 0.95 },
    visible: {
      opacity: 1,
      y: 0,
      scale: 1,
      transition: {
        duration: 0.5,
        ease: "easeOut",
      },
    },
  };

  return (
    <div className="w-full">
      {/* Animated Header */}
      <motion.div
        className="mb-6"
        initial="hidden"
        animate="visible"
        variants={headerVariants}
      >
        <h2 className="text-2xl md:text-3xl font-bold text-white mb-2">
          Game Populer
        </h2>
        <p className="text-gray-400">
          Jelajahi koleksi game terbaik dan terpopuler
        </p>
      </motion.div>

      {/* Animated Grid Layout */}
      <motion.div
        className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 md:gap-6"
        initial="hidden"
        animate="visible"
        variants={containerVariants}
      >
        {products.map((product, index) => (
          <motion.div key={product.id} variants={cardVariants}>
            <ProductCard
              title={product.title}
              description={product.description}
              price={product.price}
              originalPrice={product.originalPrice}
              genres={product.genres}
              developer={product.developer}
              rating={product.rating}
              image={product.image}
              href={product.href}
            />
          </motion.div>
        ))}
      </motion.div>
    </div>
  );
};

export default PopularGame;
