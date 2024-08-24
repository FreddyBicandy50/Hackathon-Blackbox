import { useState, useEffect } from "react";
import { copy, linkIcon, loader, tick } from "../assets";

function Demo() {
  const [article, setArticle] = useState({
    url: '',
    summary: '',
  });
  
  const handleSubmit = async (e) => {
    alert('Fetching');
  }
  
  const [url, setUrl] = useState("");
  function handleurlChange(e) {
    setUrl(e.target.value);
    console.log(url);
  }

  return (
    <section className="mt-16 w-full max-w-xl">
      <div className="flex flex-col w-full gap-6">
        <form
          className="relative flex flex-col items-center gap-4"
          onSubmit={(handleSubmit)}
        >
          <div className="relative w-full">
            <div className="-translate-x-[16rem] -translate-y-72  absolute bg-arrow bg-no-repeat h-[300px] w-[400px] "></div>

            <div className="z-1 flex gap-2 items-center justify-center bg-white w-full h-full px-3 py-2 shadow-lg rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-orange-500">
              <img src={linkIcon} alt="link_Icon" />
              <input
                type="url"
                className="flex-1 relative focus: outline-none"
                required
                placeholder="Your github repo"
              />
            </div>
          </div>

          <div className="w-full">
            <textarea
              className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none"
              placeholder="Project description"
            ></textarea>
          </div>
          <button
            type="submit"
            className="w-full py-3 mt-4 text-white rounded-lg bg-gradient-to-r from-orange-400 via-orange-500 to-yellow-500 hover:from-orange-500 hover:via-orange-600 hover:to-yellow-600 transition-colors duration-300"
          >
            Submit
          </button>
        </form>
      </div>
    </section>
  );
}

export default Demo;
