import FeaturedCard from "@/Components/FeaturedCard";
import PopularGame from "@/Components/Sections/PopularGame";
import React from "react";

const Home = () => {
  return (
    <div className="w-full h-[calc(100dvh-69px)] md:h-[calc(100dvh-76px)]">
      <FeaturedCard />
      <section className="mt-8">
        <PopularGame />
      </section>
    </div>
  );
};

export default Home;
