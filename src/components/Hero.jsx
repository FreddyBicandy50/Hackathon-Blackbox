import { logo } from "../assets";

function Hero() {
  return (
    <header className="w-full flex justify-center items-center flex-col">
      <nav className="flex justify-between items-center w-full mb-10 pt-3">
        <img src={logo} alt="sumz_logo" className="w-28 object-contain" />
        <button
          type="button"
          onClick={() =>
            window.open("https://github.com/FreddyBicandy50/blackboxHackathon")
          }
          className="black_btn"
        >
          Github
        </button>
      </nav>
      <h1 className="head_text">
        Project/Unit
        <br className="max-md:hidden" />
        Testing tool
        <span className="orange_gradient">
          <br className="max-md:hidden" />
          CI/CD
        </span>
      </h1>
      <h2 className="desc">
        <i>AI tool to help your business test your code before pushing into </i>
        production 
      </h2>
    </header>
  );
}

export default Hero;
