<template>
  <div>
    <base-card>
      <div class="links">
        <table style="width: 100%">
          <tr>
            <th>
              <router-link to="/about"
                ><h1>About me</h1></router-link
              >
            </th>
            <th>
              <router-link to="/usermanual"><h1>User manual</h1></router-link>
            </th>
          </tr>
        </table>
      </div>
      <p>
        First of all, <b>this is a demo app</b>. That means it&#39;s not created
        to actually solve some major issues, or be distributed among others, but
        to demonstrate my personal capabilities of creating certain type of
        projects and actually work in a development department. Having that in
        mind, I would suggest you
        <b>don&#39;t pay too much of attention to the design itself</b>. I&#39;m
        not a designer and I&#39;m using only a little bit of prettier design
        than the basic one.
      </p>
      <p>
        Head down to my
        <b
          ><a href="https://github.com/aleksbn" target="_blank"
            >git repository</a
          ></b
        >
        and download this
        <b
          ><a href="https://github.com/aleksbn/TimeSheetApp" target="_blank"
            >project</a
          ></b
        >. You can notice there are both backend and frontend parts of this app.
        The SPA (single page application) is frontend, and Backend is, well,
        backend. What do you need in order to start the app? You need
        <b>an IIS, a SQL server and a node.js server</b>. There is no specific
        sequence of installation you suppose to follow. You can install all of
        them in any sequence you want to.
      </p>
      <p>
        This app is just for the demo purposes, so this manual is about starting
        it on your own computer. If you actually like the project and see fits
        to use it, you can contact me and require version adapted to your own
        needs. So, here we go…
      </p>
      <h3>1. SQL server</h3>
      <p>
        Download both
        <a
          href="https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x409&culture=en-us&country=us"
          >server</a
        >
        and
        <a href="https://aka.ms/ssmsfullsetup">server management studio</a> and
        install them. In case you need help with the installation, here’s the
        link:
      </p>
      <div style="text-align: center">
        <iframe
          width="640"
          height="360"
          src="https://www.youtube.com/embed/kGdTg-vGs-E"
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          allowfullscreen
        ></iframe>
      </div>
      <p>
        After the installation, open SQL server management studio and log in
        with your credentials. You&#39;ll get the following window:
      </p>
      <p>
        <img src="../../media/images/SQL1.jpg" style="width: 100%" alt="" />
      </p>
      <p>
        Open Databases folder in the Object Explorer on the left side of the
        window.
      </p>
      <p>
        <img src="../../media/images/SQL2.jpg" style="width: 100%" alt="" />
      </p>
      <p>
        Right-click on the Databses and select <b>“New database…”</b> and type
        in <b>“TimeSheetDB”</b> in the <b>“Database name”</b> field:
      </p>
      <p>
        <img src="../../media/images/SQL3.jpg" style="width: 100%" alt="" />
      </p>
      <h3>2. Next, you need IIS server to mount the backend app.</h3>
      <p>
        I would suggest installing Visual Studio as it is the easiest
        configuration process, plus, you can edit code to suits your needs.
        Here&#39;s the
        <a
          href="https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false"
          target="_blank"
          >link</a
        >
        for downloading, and here&#39;s the instruction video:
      </p>
      <div style="text-align: center">
        <iframe
          width="640"
          height="360"
          src="https://www.youtube.com/embed/-FhQM01qsBY"
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          allowfullscreen
        ></iframe>
      </div>
      <p>
        Once you have it installed, start the solution file under the
        <b>TimeSheetApp.sln</b> name and you&#39;ll see the
        <b>backend</b> project with all of it&#39;s files.
      </p>
      <p>
        Open the file <b>“appsettings.json”</b> and edit the line number 10
        <b>(DefaultConnection)</b>.
      </p>
      <p>
        <img src="../../media/images/VS1.jpg" style="width: 100%" alt="" />
      </p>
      <p>
        Instead of my <b>DataSource</b>, you need to paste in
        <b>your server name</b>. You can find it if your
        <b>SQL server management studio</b>, when you right click on the
        <b>server name</b> in <b>Object Explorer</b> and select
        <b>Properties</b>.
      </p>
      <p>
        That&#39;s it, as fas as the backend goes! Just press the F5 (make sure
        the <b>https profile</b> is selected) and you&#39;re done! <b>IIS</b> is
        working!
      </p>
      <h3>3. The final part is installing Node.js for the frontend part.</h3>
      <p>
        Here&#39;s the
        <a
          href="https://nodejs.org/dist/v18.16.1/node-v18.16.1-x64.msi"
          target="_blank"
          >link</a
        >
        for download, and the instructional video:
      </p>
      <div style="text-align: center">
        <iframe
          width="640"
          height="360"
          src="https://www.youtube.com/embed/Gu9KdbpzSvk"
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          allowfullscreen
        ></iframe>
      </div>
      <p>
        When you&#39;re done with the installation, start it from either the
        <b>terminal in your frontend code editor</b> (I&#39;m using Visual
        Studio Code), or the <b>basic command prompt</b>.
        <i
          >Just make sure you&#39;re in the frontend project folder before
          starting it.</i
        >
        You can start it with typing in the following command (for windows
        users): <b>npm run serve</b>
      </p>
      <p>
        And that&#39;s it! Once you&#39;re done with all of this, open browser
        and type in
        <a href="http://localhost:8080/" target="_blank"
          >http://localhost:8080/</a
        >
        and it will open the app.
      </p>
    </base-card>
  </div>
</template>

<style scoped>
p {
  text-align: justify;
}
iframe {
  aspect-ratio: 16 / 9;
  height: 90%;
  width: 90%;
}
.links h1{
  display: inline;
}
.links a:visited, a:link {
  text-decoration: none;
}

</style>
